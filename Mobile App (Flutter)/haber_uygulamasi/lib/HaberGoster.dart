import 'dart:convert';
import 'package:http/http.dart' as baglanti;
import 'package:flutter/material.dart';
import 'class/haber.dart';
import 'package:flutter_html/flutter_html.dart';

class HaberGoster extends StatefulWidget {
  final int id;
  HaberGoster({Key key, @required this.id}) : super(key: key);
  @override
  _HaberGosterState createState() => _HaberGosterState();
}

class _HaberGosterState extends State<HaberGoster> {
  @override
  Widget build(BuildContext context) {
    final String baseUrl = "http://192.168.1.10/HaberApi/api";
    baglanti.post("$baseUrl/haber/" + widget.id.toString());
    return Scaffold(
      appBar: AppBar(
        title: Text("Haber Deneme"),
      ),
      body: Center(
          child: FutureBuilder<Haber>(
              future: habergetirtek(widget.id),
              builder: (context, snapshot) {
                if (snapshot.hasData) {
                  return SingleChildScrollView(
                    child: Container(
                      padding: EdgeInsets.all(8),
                      child: Column(
                        children: <Widget>[
                          Text(
                            snapshot.data.baslik,
                            style: Theme.of(context).textTheme.headline4,
                          ),
                          Text(
                            snapshot.data.ozet,
                            style: Theme.of(context).textTheme.headline6,
                          ),
                          Image.memory(
                              Base64Codec().decode(snapshot.data.resim)),
                          Html(data: snapshot.data.icerik),
                        ],
                      ),
                    ),
                  );
                } else if (snapshot.hasError)
                  return Center(
                    child: Text("Hata oluştu"),
                  );
                return Center(
                  child: CircularProgressIndicator(),
                );
              })),
    );
  }
}
