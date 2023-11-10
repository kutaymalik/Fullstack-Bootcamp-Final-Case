# Fullstack-Bootcamp-Final-Case
Bu repository Vakıfbank - Patika FullStack programının final ödevidir. Projede temel amaç iki tür kullanıcının olduğu (admin(şirket) - bayii) bir sipariş sistemi oluşturmaktır. 

## Projeyi Ayağa Kaldırma Adımları 
## Backend
- Projeyi klonlayarak veya zip şeklinde indirerek kendi cihazınıza kopyalayın.
- MSSql'i açarak yeni bir Database oluşturun(İsim: OaDb). (Backend MSSql ile çalıştığı için çalıştıralacak cihazda MSSql bulunmalıdır.)
  ![create database 1](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/8062fdcd-c335-4ede-8fd4-b4988080ff09)
  ![create database 2](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/8facdd58-ecf3-41f0-8bf0-d1861b9e2b7b)
- Daha sonra OrderAutomationsProject dosyayı içinde bulunan sln dosyasını Visual Studio 2022'de açın.
- Visual Studio'nun Server Explorer'ını açın (üstteki search çubuğundan bulabilirsiniz.) Oluşturduğunuz db'nin pathini properties'ten kopyalayın.
  ![choose db2](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/2cdb8df6-8052-483b-828a-3aba63826103)
- ![choose db3](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/1fe6d03e-8d8a-4aa8-b6f1-b0c9ce3b898e)
- Sonrasında Api katmanında bulunan apsettings.json adlı dosyayı açarak MsSqlConnection adlı alandaki pathi değiştirin ve dosyayı kaydediniz.
  ![db path](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/d7127c3e-f981-4a93-8cc3-4b7412b9b999)
- Developer Powershell terminalini açarak (dotnet ef migrations add InitialMigration --verbose --project OrderAutomationsProject.Data --startup-project OrderAutomationsProject.Api
) komutunu çalıştırınız
![add migration](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/42d8ea21-2e5a-47f5-9129-fb81e382abc4)
- Update Databse komutunu çalıştırınız
  ![update database](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/89d3addb-f1c3-46f4-8ac8-12a1cc6872de)
- Backend kullanıma hazır. Üstteki https seçeneği ile projeyi çalıştırınız.
![to work](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/b48949f5-891d-48d8-9295-9cced10d16e3)

## Frontend
- VsCode'u açarak frontend dosyasını açınız.
  ![open frontend](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/b3af11a2-6458-4068-a65b-4beb0819228a)
- Command Prompt terminalini açınız (CTRL + J)
  ![open vscode terminal](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/f0912b01-17af-4b58-b307-d90d4e3b5827)
- Gereklilikleri (npm install) indiriniz.
  ![download requirements](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/f7fc42c6-7272-40dd-870b-b7f5048a3d04)
- Frontend çalışmaya hazır !
![frontend works](https://github.com/kutaymalik/Fullstack-Bootcamp-Final-Case/assets/56682209/1277bf9c-d2f7-4570-b5aa-6b148fcd98c6)

