# Use a imagem oficial do Microsoft SQL Server
FROM quickview mcr.microsoft.com/azure-sql-edge

# Defina variáveis de ambiente
ENV SA_PASSWORD="BookStore123"
ENV ACCEPT_EULA=Y
ENV MSSQL_TCP_PORT=1433

# Exponha a porta 1433
EXPOSE 1433

# Comando para iniciar o SQL Server
CMD /opt/mssql/bin/sqlservr