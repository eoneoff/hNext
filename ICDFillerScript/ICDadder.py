import csv
import pyodbc
import codecs
import itertools

class DbRecord:
    id = itertools.count(1)
    category='empty'
    subcategory='empty'
    primary='empty'
    def __init__(self, record):
        if record['category'] != '' and record['category'] != DbRecord.category:
            DbRecord.category = record['name']
        elif record['subcategory'] != '' and  record['subcategory'] != DbRecord.subcategory:
            DbRecord.subcategory = record['name']
        elif record['primary'] != '' and record['primary'] != DbRecord.primary:
            DbRecord.primary = record['name']
        else:
            self.id = next(DbRecord.id)
            self.letter = record['secondary'][0]
            self.primary = int(record['secondary'][1:3])
            self.secondary = int(record['secondary'][4:])
            self.category = DbRecord.category[9:]
            self.subcategory = DbRecord.subcategory[9:]
            self.primaryName = DbRecord.primary[5:]
            self.name = record['name']

    def __str__(self):
        return f"{self.id} {self.category} {self.subcategory} {self.letter}{self.primary:02}.{self.secondary} {self.name}"



def readCSV(filename):
    with codecs.open(filename, 'r', 'utf-8') as file:
        fields = ['category', 'subcategory','primary','secondary','blank','name']
        reader = csv.DictReader(file, fieldnames = fields, delimiter=';')
        return list(reader)

def makeRecords(rawCSV):
    result = []
    DbRecord.id = itertools.count(1)
    for row in rawCSV:
        record = DbRecord(row)
        if(hasattr(record, 'name')):
            result.append(record)
    return result

def loadToDb(records):
    connString = 'Driver={ODBC Driver 17 for SQL Server};Server=DESKTOP-FD6IG6K;Database=hNextDb;Trusted_Connection=yes'
    insertString = 'INSERT INTO ICD(Id, Letter, PrimaryNumber, SecondaryNumber, Category, SubCategory, PrimaryName, Name) VALUES(?,?,?,?,?,?,?,?)'
    with pyodbc.connect(connString, autocommit = True) as conn:
		cursor = conn.cursor()
		cursor.fast_executemany = True
		cursor.executemany(insertString, map(lambda row: (row.id, row.letter, row.primary, row.secondary, row.category, row.subcategory, row.primaryName, row.name), records))


if __name__ == '__main__':
    loadToDb(makeRecords(readCSV('mkb.csv')))