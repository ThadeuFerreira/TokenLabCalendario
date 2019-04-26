# TokenLabCalendario
Startup  Mysql server:
- Se a maquina tiver docker execute o comando: 
  
  `docker run --name calendario-mysql -v /my/own/datadir:/var/lib/mysql -e MYSQL_ROOT_PASSWORD=root -e MYSQL_DATABASE=calendario -e MYSQL_USER=myuser -e MYSQL_PASSWORD=mysql -p "3306:3306" -d mysql:latest`

- Caso tenha servidor mysql instalado e inicializado no linux:

`mysql -h localhost -u root -p<rootpass>`

`create database calendario;`

`CREATE USER 'myuser'@'localhost' IDENTIFIED BY 'mysql';`

`GRANT ALL PRIVILEGES ON *.* TO 'myuser'@'localhost' IDENTIFIED BY 'mysql';`

Na pasta TokenLabCalendario/TokenLabCalendario execute:

`dotnet run`

As paginas e APIs vão estar disponiveis em localhost:5000
