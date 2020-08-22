import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:haber_uygulamasi/class/haber.dart';
import 'package:haber_uygulamasi/drawer.dart';
import 'package:carousel_slider/carousel_slider.dart';
import 'HaberGoster.dart';

class MyHomePage extends StatefulWidget {
  @override
  _MyHomePageState createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {
  @override
  Widget build(BuildContext context) {
    return RefreshIndicator(
        child: WillPopScope(
            child: Container(
              child: Scaffold(
                appBar: AppBar(
                  title: Text("Haber Deneme"),
                ),
                drawer: drawer(context),
                body: Center(
                  child: Container(
                      alignment: Alignment.topCenter,
                      child: SingleChildScrollView(
                        child: Column(
                          children: <Widget>[
                            slidergetir(),
                            ensonhaberlergetir(),
                          ],
                        ),
                      )),
                ),
              ),
            ),
            onWillPop: () => null),
        onRefresh: () {
          setState(() {
            habergetirhepsi("slider");
          });
          return habergetirhepsi("slider");
        });
  }
}

Widget slidergetir() {
  return new FutureBuilder<List<Haber>>(
      future: habergetirhepsi("slider"),
      builder: (context, snapshot) {
        if (snapshot.hasData) {
          return CarouselSlider.builder(
            itemBuilder: (context, index) {
              return SingleChildScrollView(
                child: GestureDetector(
                    onTap: () => Navigator.push(
                          context,
                          MaterialPageRoute(
                              builder: (context) =>
                                  HaberGoster(id: snapshot.data[index].id)),
                        ),
                    child: Card(
                      child: Column(
                        children: <Widget>[
                          Image.memory(Base64Codec()
                              .decode(snapshot.data[index].kucukResim)),
                          Container(
                            margin: EdgeInsets.all(3),
                            child: Text(
                              snapshot.data[index].baslik,
                              style: Theme.of(context).textTheme.headline6,
                            ),
                          ),
                          Container(
                            margin: EdgeInsets.all(3),
                            child: Text(
                              snapshot.data[index].ozet,
                              style: Theme.of(context).textTheme.bodyText1,
                            ),
                          ),
                          Row(
                            mainAxisAlignment: MainAxisAlignment.end,
                            crossAxisAlignment: CrossAxisAlignment.end,
                            children: <Widget>[
                              Icon(Icons.remove_red_eye),
                              Text(
                                  snapshot.data[index].goruntulenme.toString()),
                            ],
                          ),
                        ],
                      ),
                    )),
              );
            },
            options: CarouselOptions(
              height: 400,
              aspectRatio: 16 / 9,
              viewportFraction: 0.8,
              initialPage: 0,
              enableInfiniteScroll: true,
              reverse: false,
              autoPlay: true,
              autoPlayInterval: Duration(seconds: 3),
              autoPlayAnimationDuration: Duration(milliseconds: 800),
              autoPlayCurve: Curves.fastOutSlowIn,
              enlargeCenterPage: true,
              scrollDirection: Axis.horizontal,
            ),
            itemCount: snapshot.data?.length ?? 0,
          );
        } else if (snapshot.hasError) return Text("Hata oluştu");
        return Center(child: CircularProgressIndicator());
      });
}

Widget ensonhaberlergetir() {
  return FutureBuilder<List<Haber>>(
      future: habergetirhepsi("ensonhaber"),
      builder: (context, snapshot) {
        if (snapshot.hasData) {
          return ListView.builder(
            itemBuilder: (context, index) {
              return GestureDetector(
                onTap: ()=>Navigator.push(
                        context,
                        MaterialPageRoute(
                            builder: (context) =>
                                HaberGoster(id: snapshot.data[index].id)),
                      ),
                child: Card(
                  child: ListTile(                    
                    leading: Image.memory(Base64Codec()
                            .decode(snapshot.data[index].kucukResim)),
                    title: Text(snapshot.data[index].baslik),
                    subtitle: Text(snapshot.data[index].ozet),
                  ),
                ),
              );
            },
            itemCount: snapshot.data?.length ?? 0,
            shrinkWrap: true, // todo comment this out and check the result
            physics: ClampingScrollPhysics(),
          );
        } else if (snapshot.hasError)
          return Center(
            child: Text("Hata oluştu"),
          );
        return Center(
          child: CircularProgressIndicator(),
        );
      });
}
