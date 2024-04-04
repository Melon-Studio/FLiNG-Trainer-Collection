from urllib import parse
from bs4 import BeautifulSoup
import json
from tqdm import trange
import requests


class HTTPAPIRequest:
    def __init__(self):
        self.headers = {'Content-Type': 'application/json'}
        self.timeout = 10
        self.max_retries = 3

    def get_json_data(self, api_url, bearer_token, proxy=None):
        self.headers['Authorization'] = f'Bearer {bearer_token}'

        try:
            session = requests.session()
            session.keep_alive = False
            if proxy is not None:
                session.proxies = proxy
            session.headers = self.headers
            retries = 0
            while retries < self.max_retries:
                try:
                    resp = session.get(api_url, timeout=self.timeout)
                    resp.raise_for_status()
                    return resp.json()
                except requests.exceptions.RequestException as e:
                    print(f"Error: {e}. Retrying...")
                    retries += 1
            print(f"Failed to retrieve data from {api_url} after {self.max_retries} retries.")
            return None
        except requests.exceptions.RequestException as e:
            print(f"Error: {e}")
            return None


soup = BeautifulSoup(open('./html/game.html', encoding='utf-8'), features='html.parser')
a_tags = soup.findAll('a')
links_data = []

api_url = 'https://www.steamgriddb.com/api/v2/search/autocomplete/'
cover_api_url = 'https://www.steamgriddb.com/api/v2/grids/game/'
api_token = open('./token/STEAM_GRID_DB_TOKEN', 'r').read()

for index in trange(len(a_tags), desc='Getting: ', leave=True, colour='#FFFF66'):
    tag = a_tags[index]
    text = tag.text.strip()
    href = tag.get('href')
    cover_id = None
    cover_url = None

    api_request = HTTPAPIRequest()
    proxy = {
        'http': 'http://127.0.0.1:7890',
        'https': 'https://127.0.0.1:7890'
    }
    json_data = api_request.get_json_data(api_url + parse.quote(tag.text.strip().replace(' ', '%20').replace('&', '%26')), api_token, proxy=None)

    if json_data is not None and 'data' in json_data and isinstance(json_data['data'], list) and len(json_data['data']) > 0:
        cover_id = json_data['data'][0]['id']
        print('Get ID: ', cover_id)

        cover_json_data = api_request.get_json_data(cover_api_url + str(cover_id), api_token, proxy=None)

        if cover_json_data is not None and 'data' in cover_json_data and isinstance(cover_json_data['data'], list) and len(cover_json_data['data']) > 0:
            cover_url = cover_json_data['data'][0]['thumb']
            print('Get cover: ', cover_url)
    else:
        print(f'Failed to get ID for: {text}')

    link_data = {
        'id': a_tags.index(tag),
        'en_name': text,
        'trainer_url': href,
        'game_cover_id': str(cover_id),
        'game_cover_img': cover_url
    }

    links_data.append(link_data)

with open('./data/game_list.json', 'w', encoding='utf-8') as json_file:
    json.dump(links_data, json_file, ensure_ascii=False, indent=4)
