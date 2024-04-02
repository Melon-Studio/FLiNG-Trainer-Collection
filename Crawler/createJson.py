# 读取 HTML 文件
from bs4 import BeautifulSoup
import json
from tqdm import trange
import requests


class HTTPAPIRequest:
    def __init__(self):
        self.headers = {'Content-Type': 'application/json'}

    def get_json_data(self, api_url, bearer_token, params=None, proxy=None):
        self.headers['Authorization'] = f'Bearer {bearer_token}'

        try:
            response = requests.get(api_url, headers=self.headers, params=params, proxies=proxy)
            response.raise_for_status()

            return response.json()
        except requests.exceptions.RequestException as e:
            print(f"Error: - {e}")
            return None


soup = BeautifulSoup(open('./html/game.html', encoding='utf-8'), features='html.parser')
a_tags = soup.findAll('a')
links_data = []

api_url = 'https://www.steamgriddb.com/api/v2/search/autocomplete/'
api_token = open('./token/STEAM_GRID_DB_TOKEN', 'r').read()

for index in trange(len(a_tags), desc='Getting: ', leave=True, colour='#FFFF66'):
    tag = a_tags[index]
    text = tag.text.strip()
    href = tag.get('href')
    cover_id = None

    api_request = HTTPAPIRequest()
    proxy = {
        'http': 'http://127.0.0.1:7890',
        'https': 'https://127.0.0.1:7890'
    }
    json_data = api_request.get_json_data(api_url + text, api_token, proxy=proxy)

    if json_data is not None and 'data' in json_data and isinstance(json_data['data'], list) and len(
            json_data['data']) > 0:
        cover_id = json_data['data'][0]['id']
        print('Get ID:', cover_id)
    else:
        print(f'Failed to get ID for: {text}')

    link_data = {
        'index': a_tags.index(tag),
        'en_name': text,
        'trainer_url': href,
        'game_cover_id': str(cover_id)
    }

    links_data.append(link_data)

with open('./data/game_list.json', 'w', encoding='utf-8') as json_file:
    json.dump(links_data, json_file, ensure_ascii=False, indent=4)
