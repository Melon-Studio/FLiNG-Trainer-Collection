# Crawler

Get the game ID and game cover on the SteamGridDb to display the game cover

## used

To run this script, please install the recommended version of Python and dependency libraries:

- Python 3.10
- urllib3 2.2.1
- tqdm 4.66.2
- requests 2.21.0
- BeautifulSoup 4.12.3

## run

Running this script requires storing the FiLNG Trainer's Trainer List HTML file in the HTML directory of the script root directory. This script will automatically retrieve the information of the `a` tag in the HTML file.

Note: You should merge the archive list to obtain a complete Trainer list.

Provide you with a list of website addresses:

 - `https://flingtrainer.com/all-trainers-a-z/`
 - `https://flingtrainer.com/uncategorized/my-trainers-archive/`
    - `https://archive.flingtrainer.com/`
