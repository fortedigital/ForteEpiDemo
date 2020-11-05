# Epi DEMO project 
## Overview
This repository contains a demo project of EPI Server. It used as a quick start for new joiners to Forte_Digital. Here is a minimal starting project, where you can see who stuff actually work and play around. 

In order to admin your Epi Server please go to http://localhost:[YOUR_PORT]/episerver. Default user name & password can be find in  `Migrations/CreateAdministratorAccounts.cs` in solution.      

## Quick start
### Minimal required config
#### Db connection
Connection string is provided inside Web.Config connectionStrings sections. Default value point to LocalDB database. In order to set up local DB please follow instruction provide on this site: https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver15 

Feel free to change connection string according to your local development environment (SQL Express, Sql Developer or so on). 
#### EPI Server Find
Project includes Episerver Find feature. In order to run project you need to provide config inside  `episerver.find.config` config file (ignored in repository) in `EpiDemo.Web` directory. File should have following format:
```
<?xml version="1.0" encoding="utf-8"?>
 <episerver.find
     serviceUrl="YOUR_URL_GO_HERE"
     defaultIndex="YOUT_INDEX_GO_HERE" />
```

In order to create a development index please follow instruction on http://find.episerver.com/ and provider values in config accordingly. 

### Build & run
In order to build & run project you need following steps

Run npm install in EpiDemo.Web directory:

`npm install`

Build front end

`npm run build`

Run front-end development server:

`npm run start`

Run App Wep App in iss-express 

`F5 in Visual studio/Rider`  