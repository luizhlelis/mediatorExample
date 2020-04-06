FROM microsoft/mssql-server-linux:latest

RUN apt-get update && apt-get install -y  \
    curl \
    apt-transport-https

# Downloading microsoft odbc
RUN curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add -
RUN curl https://packages.microsoft.com/config/ubuntu/16.04/prod.list > /etc/apt/sources.list.d/mssql-release.list

# Installing microsoft odbc
RUN apt-get install -y locales \
    && echo "en_US.UTF-8 UTF-8" > /etc/locale.gen \
    && locale-gen

# Adding sqlcmd to bash
ENV PATH="/opt/mssql-tools/bin:${PATH}"

# Adding entry point file on tmp folder
COPY  ./wait-for-it.sh ./wait-for-it.sh
RUN chmod -R 777 wait-for-it.sh


