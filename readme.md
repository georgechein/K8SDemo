開發設定
- 因 web 與 webapi 為不同網站，所以需要開啟 CORS (Cross-Origin Resource Sharing)設定。於Nuget主控台輸入以下指令
```
Install-Package Microsoft.AspNetCore.Cors
```
- Startup.cs 中，需加入相關設定，注意需要 AddMvc() 或 UseMvc() 之前。
- 以 docker 偵錯時，環境變數設定於 docker-compose.override.yml。
- 以 IIS express 偵錯時，環境變數設定於 launchSettings.json。

docker 設定
-
- 此版本只適用於有反向代理的環境，如：Kubernetes。
- 編譯至 linux 平台時，k8sdemoapi 的 volumes ，前方路徑須修正為共用儲存空間的路徑。
- 執行前，須先建立名為 k8sdemo_nw 的網路定義，用來提供網路範圍，才能指定 IP。
```
docker network create --subnet=172.21.0.0/24 k8sdemo_nw
```
- 建置映像檔，使用以下指令：
```
docker-compose -f .\mydocker-compose.1.0.yml build --no-cache
```
- 首次啟動 container ，使用以下指令：
```
docker-compose -f .\mydocker-compose.1.0.yml up -d
```
- 啟動 container ，使用以下指令：
```
docker-compose -f .\mydocker-compose.1.0.yml run -d --service-ports
```
- 建置更版後的映像檔，使用以下指令：
```
docker-compose -f .\mydocker-compose.1.0.1.yml build --no-cache
```
- 首次啟動更版後的 container ，使用以下指令：
```
docker-compose -f .\mydocker-compose.1.0.1.yml up -d
```
- 啟動更版後的 container ，使用以下指令：
```
docker-compose -f .\mydocker-compose.1.0.1.yml run -d --service-ports
```

展示設定
-

- 展示的主機，必須把 C:\Windows\System32\drivers\etc\hosts 的檔案加上兩行。實際 IP 應隨 Kubernetes ingress 的設定做修改。
```
172.21.0.2       k8sdemoweb
172.21.0.3       k8sdemoapi
```

detectlive.sh
- 
此程式於linux下執行，可用來判斷 api server 是否仍提供服務。使用前請先修改檔案中的網址，並使用以下指令給予執行權限：
```
chmod a+x ./detectlive.sh
```
執行時，只需打
```
./detectlive.sh
```
若網站仍提供服務，則無任何回傳值。若有任何回傳值，均表示失敗。
