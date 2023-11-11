# Fullstack-Bootcamp-Final-Case (Readme güncellemesini repoyu public yapınca resimler kaybolduğu için yaptım. Ondan önceki son commitim 8 saat öncedir.)
Bu repository Vakıfbank - Patika FullStack programının final ödevidir. Projede temel amaç iki tür kullanıcının olduğu (admin(şirket) - bayii) bir sipariş sistemi oluşturmaktır. 

## Projeyi Ayağa Kaldırma Adımları 
## Backend
- Projeyi klonlayarak veya zip şeklinde indirerek kendi cihazınıza kopyalayın. <br/> <br/>


- MSSql'i açarak yeni bir Database oluşturun(İsim: OaDb). (Backend MSSql ile çalıştığı için çalıştıralacak cihazda MSSql bulunmalıdır.) <br/> <br/>
![create database 1](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/97b9dd83-e1fe-4085-82c3-d4d08c24dd0a) <br/> <br/>

![create database 2](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/54733622-6640-4c60-99ef-77f3b580f987) <br/> <br/>

  
- Daha sonra OrderAutomationsProject dosyayı içinde bulunan sln dosyasını Visual Studio 2022'de açın. <br/> <br/>

  
- Visual Studio'nun Server Explorer'ını açın (üstteki search çubuğundan bulabilirsiniz.) Oluşturduğunuz db'nin pathini properties'ten kopyalayın. <br/> <br/>
![choose db2](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/e1d97e4b-3c74-4c5f-9cdd-e49096c70b4c)<br/> <br/>
![choose db3](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/34a25cbd-537b-4c5f-85bc-76fe7fb969e9) <br/> <br/>

  
- Sonrasında Api katmanında bulunan apsettings.json adlı dosyayı açarak MsSqlConnection adlı alandaki pathi değiştirin ve dosyayı kaydediniz. <br/> <br/>
 ![db path](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/99c88d2e-ed3a-450e-aef3-f6df6fc81168)<br/> <br/>

  
- Developer Powershell terminalini açarak (dotnet ef migrations add InitialMigration --verbose --project OrderAutomationsProject.Data --startup-project OrderAutomationsProject.Api) komutunu çalıştırınız  <br/> <br/>
![add migration](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/982c9fbe-15f4-47c7-b38a-3084cc6e1197) <br/> <br/>


- Update Databse komutunu çalıştırınız (dotnet ef database update  --verbose --project OrderAutomationsProject.Data --startup-project OrderAutomationsProject.Api) <br/> <br/>
![update database](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/bfc43e12-3238-4096-8c20-f0fb97967e63)<br/> <br/>

  
- Backend kullanıma hazır. Üstteki https seçeneği ile projeyi çalıştırınız. <br/> <br/>
![to work](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/586164ee-fecc-48e1-a2ad-29828e5c1ae8)<br/> <br/>

## Frontend <br/> <br/>
- VsCode'u açarak frontend dosyasını açınız. <br/> <br/>
![open frontend](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/97524600-ecd6-4589-9958-d3357d82cfbc)<br/> <br/>

  
- Command Prompt terminalini açınız (CTRL + J) <br/> <br/>
![open vscode terminal](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/0b615dbc-f325-4c49-80e8-d7c487a378a0) <br/> <br/>

  
- Gereklilikleri (npm install) indiriniz. <br/> <br/>
![download requirements](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/ef4e5964-89c5-49f3-a404-558d286b7b1f) <br/> <br/>

  
- Frontend çalışmaya hazır ! (http://localhost:4200/)'e bağlanarak açabilirsiniz.
![frontend works](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/f459e333-a61e-4bbd-8f92-2e72f2dc81a3)

# Kullanım
- Admin ile kayıt olunuz.
![admin kayıt](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/3f83730b-1781-423e-a11e-824cd47ac50e)  <br/> <br/>

- Kayıt olunan mail ile girş yapınız
![admin giriş](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/3eacfbc6-74d5-40e3-a366-ef8169ce0fc1)  <br/> <br/>

- Kategori ve ürünler oluşturunuz
![kategori oluşturma](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/686cb6f8-997c-4535-80d9-461b3a92419e)  <br/> <br/>

![Ürün ekleme](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/2c736354-a78c-49f8-8adf-bd1de8180e7e)  <br/> <br/>

- Sağ üstten logout olarak dealer ile kayıt olup giriş yapınız.
![dealer kayıt](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/8eaf0c5f-3c8e-465f-b136-a39a9b79366e)  <br/> <br/>


- Navigasyondan card oluşturunuz.
![kart ekleme](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/e6e019d2-81a2-4065-8ef1-9acee4fc514e)  <br/> <br/>

- Navigasyondan order oluşturunuz.
![sipariş oluşturma](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/9ca18e3c-9e8c-4370-b245-7efe1d503167)  <br/> <br/>


- Admin ile giriş yapıp navigasyondaki order liste girip orderı confirm ediniz.
![order confirm cancel](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/b24998da-e5f5-4a45-8979-e1c986e6974b)

- Dealer ile giriş yaptıktan sonra eğer açık hesaptan ödeme yapmamışsanız orderlistten confirmed orderların ödemesini yapabilirsiniz.
![order ödeme](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/525a3754-49fc-4548-bc53-43947b341fe3)  <br/> <br/>

![ödeme işlemi](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/6f70f693-c55e-4336-9b0a-1064778d7d89)  <br/> <br/>

  
- Dashboardlarda istenilen yoğunluk reportları bulunmaktadır.
![sipraiş yoğunluğu raporu](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/6160b59f-5dac-4fec-9f28-6c853ed4f0da)  <br/> <br/>


- Admin ile dealer listten dealera mesaj gönderebilirsiniz.
- Dealer ile sağ üst avatara tıklayıp mesaj kısmından admine mesaj gönderebilirsiniz. Dealer sadece admine mesaj atabilir admin bütün dealerlara mesaj atabilir.
![mesaj](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/054ac3ea-fa77-4444-b6df-eef39137595c)  <br/> <br/>

- Dealer rolündeyken avatara tıklayıp bilgileri güncelleyebilirsiniz.
![bilgi güncelleme](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/6f130b65-3375-4714-ab07-78e9474e814b)  <br/> <br/>

- Admin rolünde kullanıcıların kar marjı ve açık hesap limitlerini güncelleyebilirsiniz.
- Dealer bazlı siparişleri dealer list kısmından görüntüleyebilirsiniz.
