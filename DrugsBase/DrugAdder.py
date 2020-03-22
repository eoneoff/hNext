import csv
import codecs
import pandas

import pandas as pd
import numpy as np


def readSubstances(filename):
    data = pd.read_excel(filename)
    data = data.replace(np.nan, '', regex=True)
    substances = pd.DataFrame(data[data['Фармакотерапевтична група']!='Субстанціїю']['Міжнародне непатентоване найменування'].sort_values().unique(), columns=['InternationalName'])
    substances['Name'] = ''
    substances.to_csv('DrugSubstances.csv', index=False)