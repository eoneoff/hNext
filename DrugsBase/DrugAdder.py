import pandas as pd
import numpy as np
from sqlalchemy import create_engine


def saveDrugsToDb (filename):
    data = pd.read_excel(filename)
    data = data.replace(np.nan, '', regex=True)
    no_substances = data['Фармакотерапевтична група']!='Субстанції.'
    not_in_bulk = ~data['Форма випуску'].str.contains('in bulk')
    drugs = data[no_substances & not_in_bulk][['Торгівельне найменування','Міжнародне непатентоване найменування', 'Форма випуску', 'Склад (діючі)', 'Фармакотерапевтична група', 'Код АТС 1', 'Заявник: назва українською']]
    drugs.columns = ['Name', 'InternationalName', 'Form', 'Compound', 'Group', 'ATC', 'Manufacturer']

    DB = {'serverName':'46.149.84.230', 'database':'hNextDb', 'driver':'driver=ODBC Driver 17 for SQL Server', 'user':'eoneoff','password':'Vjqgfhjkm1'}

    engine = create_engine(f'mssql+pyodbc://{DB["user"]}:{DB["password"]}@{DB["serverName"]}/{DB["database"]}?{DB["driver"]}')

    drugs.to_sql("Drugs", con=engine, if_exists='append', index=False)


print('Adding started')

try:
    saveDrugsToDb('reestr.xlsx')
except Exception as exp:
    print(exp)
else:
    print("Saved")