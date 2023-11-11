# Fullstack-Bootcamp-Final-Case
Bu repository Vakıfbank - Patika FullStack programının final ödevidir. Projede temel amaç iki tür kullanıcının olduğu (admin(şirket) - bayii) bir sipariş sistemi oluşturmaktır. 

## Projeyi Ayağa Kaldırma Adımları 
## Backend
- Projeyi klonlayarak veya zip şeklinde indirerek kendi cihazınıza kopyalayın. <br/> <br/>


- MSSql'i açarak yeni bir Database oluşturun(İsim: OaDb). (Backend MSSql ile çalıştığı için çalıştıralacak cihazda MSSql bulunmalıdır.) <br/> <br/>
  
![create database 1](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/8062fdcd-c335-4ede-8fd4-b4988080ff09) <br/> <br/>

  
![create database 2](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/8facdd58-ecf3-41f0-8bf0-d1861b9e2b7b) <br/> <br/>

  
- Daha sonra OrderAutomationsProject dosyayı içinde bulunan sln dosyasını Visual Studio 2022'de açın. <br/> <br/>

  
- Visual Studio'nun Server Explorer'ını açın (üstteki search çubuğundan bulabilirsiniz.) Oluşturduğunuz db'nin pathini properties'ten kopyalayın. <br/> <br/>
  
![choose db2](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/2cdb8df6-8052-483b-828a-3aba63826103) <br/> <br/>
  
![choose db3](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/1fe6d03e-8d8a-4aa8-b6f1-b0c9ce3b898e) <br/> <br/>

  
- Sonrasında Api katmanında bulunan apsettings.json adlı dosyayı açarak MsSqlConnection adlı alandaki pathi değiştirin ve dosyayı kaydediniz. <br/> <br/>
  
![db path](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/d7127c3e-f981-4a93-8cc3-4b7412b9b999) <br/> <br/>

  
- Developer Powershell terminalini açarak (dotnet ef migrations add InitialMigration --verbose --project OrderAutomationsProject.Data --startup-project OrderAutomationsProject.Api) komutunu çalıştırınız  <br/> <br/>

  ![add migration](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/42d8ea21-2e5a-47f5-9129-fb81e382abc4) <br/> <br/>


- Update Databse komutunu çalıştırınız (dotnet ef database update  --verbose --project OrderAutomationsProject.Data --startup-project OrderAutomationsProject.Api) <br/> <br/>

![update database](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/89d3addb-f1c3-46f4-8ac8-12a1cc6872de) <br/> <br/>

  
- Backend kullanıma hazır. Üstteki https seçeneği ile projeyi çalıştırınız. <br/> <br/>
  
![to work](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/b48949f5-891d-48d8-9295-9cced10d16e3) <br/> <br/>

## Frontend <br/> <br/>
- VsCode'u açarak frontend dosyasını açınız. <br/> <br/>

![open frontend](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/b3af11a2-6458-4068-a65b-4beb0819228a) <br/> <br/>

  
- Command Prompt terminalini açınız (CTRL + J) <br/> <br/>
  
![open vscode terminal](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/f0912b01-17af-4b58-b307-d90d4e3b5827) <br/> <br/>

  
- Gereklilikleri (npm install) indiriniz. <br/> <br/>

![download requirements](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/f7fc42c6-7272-40dd-870b-b7f5048a3d04) <br/> <br/>

  
- Frontend çalışmaya hazır ! (http://localhost:4200/)'e bağlanarak açabilirsiniz.

![frontend works](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/1277bf9c-d2f7-4570-b5aa-6b148fcd98c6)

# Kullanım
- Admin ile kayıt olunuz.
- Kayıt olunan mail ile girş yapınız
- kategori ve ürünler oluşturunuz
- Sağ üstten logout olarak dealer ile kayıt olup giriş yapınız. 
- Navigasyondan card oluşturunuz. 
- navigasyondan order oluşturunuz.
- Admin ile giriş yapıp navigasyondaki order liste girip orderı confirm ediniz.
- dealer ile giriş yaptıktan sonra eğer açık hesaptan ödeme yapmamışsanız orderlistten confirmed orderların ödemesini yapabilirsiniz.
- Dashboardlarda istenilen yoğunluk reportları bulunmaktadır. 
- admin ile dealer listten dealera mesaj gönderebilirsiniz.
- dealer ile sağ üst avatara tıklayıp mesaj kısmından admine mesaj gönderebilirsiniz. Dealer sadece admine mesaj atabilir admin bütün dealerlara mesaj atabilir.
- Dealer rolündeyken avatara tıklayıp bilgileri güncelleyebilirsiniz.

