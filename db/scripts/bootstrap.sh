#!/usr/bin/env bash

#Boxes don't have their timezone set.  This will ensure files and other items are timestamped correctly
sudo timedatectl set-timezone "America/New_York"

#export DEBIAN_FRONTEND=noninteractive

#update library list to the latest 
sudo apt-get update -y
sudo DEBIAN_FRONTEND=noninteractive apt-get -yq upgrade

#midnight commander.  It rocks!
sudo apt-get install mc -y

#will need wget and vim, because it's awesome
sudo apt-get install -y wget vim

#Get postgres respository signing key
wget --quiet -O - https://www.postgresql.org/media/keys/ACCC4CF8.asc | sudo apt-key add -

RELEASE=$(lsb_release -cs)
echo "deb http://apt.postgresql.org/pub/repos/apt/ ${RELEASE}"-pgdg main | sudo tee  /etc/apt/sources.list.d/pgdg.list

sudo apt-get update -y

#Postgres (from postres repo)
sudo apt-get -y install postgresql-12 postgresql-client-12
#Postgres dev libs (not sure this is needed)
#sudo apt-get install libpq-dev -y
#Extensions
sudo apt-get install postgresql-contrib -y

 # fix permissions
echo "-------------------- fixing listen_addresses on postgresql.conf"
sudo sed -i "s/#listen_address.*/listen_addresses '*'/" /etc/postgresql/12/main/postgresql.conf
echo "-------------------- fixing postgres pg_hba.conf file"
# replace the ipv4 host line with the above line
sudo cat >> /etc/postgresql/12/main/pg_hba.conf <<EOF
# Accept all IPv4 connections - FOR DEVELOPMENT ONLY!!!
host    all         all         0.0.0.0/0             md5
EOF

#setup Postgres
#first setup password to Ubuntu postgres user
sudo echo "postgres:helloworld" | sudo chpasswd

#update postgres postgres user
sudo -u postgres psql -U postgres -d postgres -c "alter user postgres with password 'helloworld';"

#restart postgres to ensure extensions are installed
sudo /etc/init.d/postgresql restart

#create user, db, tables, etc for the scoring
sudo -u postgres psql < /vagrant/scripts/create_db.sql
sudo -u postgres psql -d identity_db < /vagrant/scripts/create_tables.sql
sudo -u postgres psql -d identity_db < /vagrant/scripts/sample_data.sql

sudo reboot

