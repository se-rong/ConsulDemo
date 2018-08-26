## consul下载地址
https://www.consul.io/downloads.html

## windows开发环境
- 解压下载的安装包到d盘，D:\consul。
- 使用命令行运行 consul agent -dev
- UI界面地址：http://localhost:8500
- cmd 命令窗口执行:consul.exe agent -server ui -bootstrap -client 0.0.0.0 -data-dir="D:\consul" -bind X.X.X.X  
其中X.X.X.X为服务器ip,即可使用http://X.X.X.X:8500 访问ui而不是只能使用localhost连接
