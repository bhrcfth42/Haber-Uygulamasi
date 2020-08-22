import 'dart:io';
import 'package:flutter/material.dart';

Widget drawer(BuildContext context){
  return new Drawer(
          child: Column(
            children: <Widget>[
              new ListTile(leading: Icon(Icons.home),
              title: Text("Ana Sayfa"),
              onTap: ()=>Navigator.pushNamed(context, "/"),),
              new ListTile(leading: Icon(Icons.list),
              title: Text("Tüm Haberler"),
              onTap: ()=>Navigator.pushNamed(context, "/TumHaberler"),),
              new ListTile(leading: Icon(Icons.exit_to_app),
              title: Text("Çıkış"),
              onTap: ()=>exit(0),),
            ],
          ),
        );
}