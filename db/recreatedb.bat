vagrant ssh -c "sudo -u postgres psql < /vagrant/scripts/create_db.sql;sudo -u postgres psql -d identity_db < /vagrant/scripts/create_tables.sql;sudo -u postgres psql -d identity_db < /vagrant/scripts/sample_data.sql"