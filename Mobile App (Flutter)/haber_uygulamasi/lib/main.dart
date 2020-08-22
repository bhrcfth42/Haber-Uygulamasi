import 'package:flutter/material.dart';
import 'package:haber_uygulamasi/MyHomePage.dart';
import 'package:haber_uygulamasi/TumHaberler.dart';
import 'HaberGoster.dart';

void main() {
  runApp(MyApp());
}

class MyApp extends StatelessWidget {
  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Haber Uygulaması',
      theme: ThemeData(
        visualDensity: VisualDensity.adaptivePlatformDensity,
        primaryColor: Colors.deepOrange,
        textTheme: TextTheme(
          bodyText2: TextStyle(fontFamily: 'Hind'),
        ),
      ),
      darkTheme: ThemeData.dark(),
      routes: {
        "/": (context) => MyHomePage(),
        "/TumHaberler": (context) => TumHaberler(),
        "/HaberGoster": (context) => HaberGoster(
              id: null,
            ),
      },
      debugShowCheckedModeBanner: false,
    );
  }
}
