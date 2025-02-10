# 📦 Mikroservis Tabanlı E-Ticaret Uygulaması

Bu proje, **ASP.NET Core** ile geliştirilen **mikroservis mimarisine** sahip bir **e-ticaret uygulamasıdır**. Proje kapsamında **bağımsız mikroservisler** oluşturulmuş, her biri belirli işlevleri yerine getirmek üzere tasarlanmıştır. **Docker ve Ocelot API Gateway** entegrasyonu sağlanmış, **Identity Server 4** kullanılarak kimlik doğrulama işlemleri gerçekleştirilmiştir.

---

## 🚀 Proje Genel Özellikleri
✅ **Mikroservis Mimarisi** ile esnek ve ölçeklenebilir yapı  
✅ **Kimlik doğrulama ve yetkilendirme** için **Identity Server 4 ve JWT**  
✅ **Ocelot API Gateway** ile tek noktadan servis yönetimi  
✅ **Docker & Portainer** ile servislerin bağımsız ve izole çalıştırılması  
✅ **Redis, MSSQL, PostgreSQL ve MongoDB** gibi farklı **veritabanı teknolojilerinin** kullanımı  
✅ **RabbitMQ** ile mesaj kuyruğa alma ve okuma (İlerleyen aşamalarda kullanılacaktır)  
✅ **Google Cloud Storage** ile **ürün görsellerinin** bulut ortamında saklanması  
✅ **SignalR** ile admin panelinde **canlı yorum güncelleme** sistemi  
✅ **MailKit** ile kullanıcıya **mail gönderme** işlemi  
✅ **RapidAPI** ile **hava durumu, döviz kurları ve en çok puanlanan ürünlerin** çekilmesi  
✅ **AJAX** kullanarak **sayfa yenilenmeden anlık yorum ekleme** işlemi  
✅ **Entity Framework Core ve Dapper** ile **iki farklı ORM** kullanımı  

---

## 🏗 Kullanılan Teknolojiler

| Teknoloji          | Açıklama |
|--------------------|----------|
| **ASP.NET Core**  | Full Stack Geliştirme |
| **Entity Framework Core** | ORM (Veritabanı yönetimi) |
| **Dapper** | Hafif ORM alternatifi |
| **Ocelot API Gateway** | Mikroservislerin yönetilmesi |
| **Docker & Portainer** | Konteynerleştirme ve yönetim |
| **MSSQL, MongoDB, PostgreSQL, Redis** | Farklı mikroservisler için kullanılan veritabanları |
| **RabbitMQ** | Mesaj kuyruğu (İlerleyen aşamalarda kullanılacak) |
| **SignalR** | Gerçek zamanlı veri iletimi (Yorum güncellemeleri için) |
| **MailKit** | Kullanıcılara e-posta gönderimi |
| **Google Cloud Storage** | Ürün görsellerinin saklanması |
| **RapidAPI** | Harici API'lerden veri çekme |
| **AJAX** | Dinamik veri yükleme (Örn: Yorum ekleme) |

---

## 📂 Mikroservisler

| Mikroservis | Açıklama | Veritabanı |
|-------------|---------|------------|
| **Basket**  | Kullanıcıların ürünleri sepete eklemesi | Redis |
| **Cargo**   | Kargo işlemleri ve takibi | MSSQL |
| **Catalog** | Ürün ve kategori yönetimi | MongoDB |
| **Comment** | Kullanıcı yorumları ve değerlendirmeleri | MSSQL |
| **Discount** | İndirim kuponu yönetimi | MSSQL |
| **Images** | Ürün görsellerinin yönetimi | Google Cloud Storage |
| **Message** | Kullanıcı mesajları ve destek sistemi | PostgreSQL |
| **Order** | Sipariş oluşturma ve takip | MSSQL |
| **Payment** | Ödeme işlemleri simülasyonu | MSSQL |
| **IdentityServer** | Kimlik doğrulama ve yetkilendirme | MSSQL |

---

## 📌 Kullanım Senaryosu

1️⃣ **Kullanıcı**, sisteme **kayıt olur ve giriş yapar**.  
2️⃣ **Ana sayfada listelenen ürünleri** inceler ve **sepete ekler**.  
3️⃣ **Sepetteki ürünlere indirim kodu uygulayabilir**.  
4️⃣ **Ödeme sayfasında**, simülasyon kart bilgilerini girerek sipariş oluşturur.  
5️⃣ **Sipariş bilgileri**, **kargo ve sipariş yönetim mikroservislerine** iletilir.  
6️⃣ Kullanıcı, **siparişlerini ve mesajlarını "Hesabım" panelinden görüntüler**.  
7️⃣ **Admin panelinden**, admin **kategori, ürün, marka ve ana sayfa içeriklerini** **dinamik olarak** yönetir.  
8️⃣ Kullanıcılar, **ürünlere yorum yapabilir** ve bu yorumlar **SignalR** ile **admin panelinde anlık olarak güncellenir**.  
9️⃣ Kullanıcıya **MailKit ile otomatik mail gönderilir**.  
🔟 **Google Cloud Storage** ile ürün görselleri **bulut ortamında saklanır**.  
1️⃣1️⃣ **Hava durumu, döviz kurları ve en çok puanlanan ürünler**, **RapidAPI** üzerinden çekilir.  

---

## 📸 Proje Görselleri

### 🔑 Giriş Ekranı
![1](https://github.com/user-attachments/assets/404d541a-98c2-41c1-bcfb-750bd1f64bb8)

### 🏠 Ana Sayfa
![2](https://github.com/user-attachments/assets/b882d9c6-093a-46b4-8603-28b3d3ccf9fe)

### 🏷️ Markalar ve Sayfa Alt Bilgisi (Footer)
![3](https://github.com/user-attachments/assets/186f0e76-9eed-4b7b-a98f-1f0c32e7da88)

### 🛍️ Ürün Detay Sayfası
![4](https://github.com/user-attachments/assets/ad874bbe-8d04-45a1-a29c-91c6e34b07f6)

### 💬 Kullanıcı Yorumları
![5](https://github.com/user-attachments/assets/4678aaa5-3152-472c-8d9d-9c4faaf939c9)

### 🛒 Sepet Sayfası
![6](https://github.com/user-attachments/assets/6fd389b8-12de-46df-a0bf-54ca5945c514)

### 📍 Adres Bilgileri Sayfası
![7](https://github.com/user-attachments/assets/4910c5a4-f3ba-48ce-bb81-be5eb3bb8287)

### 💳 Ödeme Sayfası
![8](https://github.com/user-attachments/assets/0afc67f5-e9ad-4abd-a742-889f3c1eaa2d)

### ✅ Sipariş Onay Sayfası
![9](https://github.com/user-attachments/assets/64231b2b-8240-454f-bcfa-75f2db30e8d2)

### 📞 İletişim Sayfası
![10](https://github.com/user-attachments/assets/6a59deab-3da0-457f-9055-abfc2a3453bd)

### 📊 Admin Paneli - İstatistikler
![11](https://github.com/user-attachments/assets/1117a8ef-9e08-4641-b95f-d379ad83c80c)

### 👤 Kullanıcı Paneli
![12](https://github.com/user-attachments/assets/843cb566-b972-4be5-90ec-2c5057565ff5)

## 📂 Proje Kurulumu

### 1️⃣. **Projeyi Klonlayın**
```sh
git clone https://github.com/kullanici-adi/mikroservis-e-ticaret.git
cd mikroservis-e-ticaret
