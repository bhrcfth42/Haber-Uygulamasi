import 'package:http/http.dart' as baglanti;
import 'dart:async';
import 'dart:convert';

class Haber {
  int id;
  String baslik;
  String ozet;
  String icerik;
  String yayimTarihi;
  String yazarID;
  int kategoriID;
  int tipID;
  String resim;
  String kucukResim;
  int goruntulenme;
  Null aspnetUsers;
  Null kategori;
  Null tip;

  Haber(
      {this.id,
      this.baslik,
      this.ozet,
      this.icerik,
      this.yayimTarihi,
      this.yazarID,
      this.kategoriID,
      this.tipID,
      this.resim,
      this.kucukResim,
      this.goruntulenme,
      this.aspnetUsers,
      this.kategori,
      this.tip});

  Haber.fromJson(Map<String, dynamic> json) {
    id = json['Id'];
    baslik = json['Baslik'];
    ozet = json['Ozet'];
    icerik = json['Icerik'];
    yayimTarihi = json['YayimTarihi'];
    yazarID = json['YazarID'];
    kategoriID = json['KategoriID'];
    tipID = json['TipID'];
    resim = json['Resim'];
    kucukResim = json['KucukResim'];
    goruntulenme = json['Goruntulenme'];
    aspnetUsers = json['aspnet_Users'];
    kategori = json['Kategori'];
    tip = json['Tip'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['Id'] = this.id;
    data['Baslik'] = this.baslik;
    data['Ozet'] = this.ozet;
    data['Icerik'] = this.icerik;
    data['YayimTarihi'] = this.yayimTarihi;
    data['YazarID'] = this.yazarID;
    data['KategoriID'] = this.kategoriID;
    data['TipID'] = this.tipID;
    data['Resim'] = this.resim;
    data['KucukResim'] = this.kucukResim;
    data['Goruntulenme'] = this.goruntulenme;
    data['aspnet_Users'] = this.aspnetUsers;
    data['Kategori'] = this.kategori;
    data['Tip'] = this.tip;
    return data;
  }
}

Future<List<Haber>> habergetirhepsi(String yazi) async {
  final String baseUrl = "http://192.168.1.10/HaberApi/api";
  final response = await baglanti.get("$baseUrl/" + yazi);
  var list = json.decode(response.body) as List;
  List<Haber> haber = list.map((i) => Haber.fromJson(i)).toList();
  if (response.statusCode == 200) {
    return haber;
  } else {
    return null;
  }
}

Future<Haber> habergetirtek(int deger) async {
  final String baseUrl = "http://192.168.1.10/HaberApi/api";
  final response = await baglanti.get("$baseUrl/haber/" + deger.toString());
  if (response.statusCode == 200) {
    return Haber.fromJson(json.decode(response.body));
  } else {
    return null;
  }
}
