# Use a imagem oficial do Microsoft SQL Server
FROM mcr.microsoft.com/mssql/server:2019-latest

# Defina variáveis de ambiente
ENV SA_PASSWORD=$BookStore123
ENV ACCEPT_EULA=Y
ENV MSSQL_TCP_PORT=1439

# Exponha a porta 1439
EXPOSE 1439

# Comando para iniciar o SQL Server
CMD /opt/mssql/bin/sqlservr