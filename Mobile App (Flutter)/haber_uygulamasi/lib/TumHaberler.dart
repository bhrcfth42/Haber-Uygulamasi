import 'package:flutter/material.dart';
import 'package:haber_uygulamasi/class/haber.dart';
import 'dart:convert';
import 'package:haber_uygulamasi/drawer.dart';
import 'package:haber_uygulamasi/HaberGoster.dart';

class TumHaberler extends StatefulWidget {
  @override
  _TumHaberlerState createState() => _TumHaberlerState();
}

class _TumHaberlerState extends State<TumHaberler> {
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
                body: tumhaberler()),
          ),
          onWillPop: () => null),
      onRefresh: () {
        setState(() {
          habergetirhepsi("haber");
        });
        return habergetirhepsi("haber");
      },
    );
  }
}

Widget tumhaberler() {
  return Center(
    child: FutureBuilder<List<Haber>>(
      future: habergetirhepsi("haber"),
      builder: (context, snapshot) {
        if (snapshot.hasData) {
          return ListView.builder(
            padding: EdgeInsets.all(8),
            itemBuilder: (context, index) {
              return GestureDetector(
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
                        Row(
                          mainAxisAlignment: MainAxisAlignment.end,
                          children: <Widget>[
                            Icon(Icons.remove_red_eye),
                            Text(snapshot.data[index].goruntulenme.toString()),
                          ],
                        ),
                      ],
                    ),
                  ));
            },
            itemCount: snapshot.data?.length ?? 0,
            shrinkWrap: true, // todo comment this out and check the result
            physics:
                ClampingScrollPhysics(), // todo comment this out and check the result
          );
        } else if (snapshot.hasError)
          return Center(
            child: Text("Hata oluştu"),
          );
        return Center(
          child: CircularProgressIndicator(),
        );
      },
    ),
  );
}
