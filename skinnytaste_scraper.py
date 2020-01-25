from bs4 import BeautifulSoup
import requests
import csv

class SkinnyTasteScraper:
    def __init__(self, url_string):
        self.Soup = BeautifulSoup(requests.get(url_string).text, 'lxml')
        self.RecipeTitle = self.Soup.find('h2', class_='wprm-recipe-name').text
        self.Summary = self.Soup.find('div', class_='wprm-recipe-summary').text
        self.Yield = self.Soup.find('span', class_='wprm-recipe-servings').text
        self.Ingredients = self.get_ingredients()
        self.Instructions = self.get_instructions()

    def get_ingredients(self):
        ingredients_string = ''
        for ingredient in self.Soup.find_all('li', {'class': 'wprm-recipe-ingredient'}):
            ingredients_string += f"{ingredient.text}" + "\n"
        return ingredients_string
    
    def get_instructions(self):
        steps_string = ''
        instructions_list = []
        for instruct in self.Soup.find_all('li', {'class': 'wprm-recipe-instruction'}):
            instructions_list.append(instruct.text)
        for i in range(len(instructions_list)):
            steps_string += f"{(i+1)}. {instructions_list[i]}" + "\n"
        return steps_string

    def write_to_cvs(self, writer):
        writer.writerow([self.RecipeTitle, self.Summary, self.Yield, self.Ingredients, self.Instructions])

def main():
    cvs_file = open('recipes.csv', 'w')
    csv_writer = csv.writer(cvs_file)
    csv_writer.writerow(['title', 'summary', 'yield', 'ingredients', 'instructions'])

    brown_rice = SkinnyTasteScraper('https://www.skinnytaste.com/how-to-make-perfect-brown-rice-every/')
    deviled_eggs = SkinnyTasteScraper('https://www.skinnytaste.com/deviled-eggs/')
    butternut_mac = SkinnyTasteScraper('https://www.skinnytaste.com/butternut-squash-mac-and-cheese/')
    banana_bread = SkinnyTasteScraper('https://www.skinnytaste.com/makeover-banana-nut-bread-3-pts/')
    buff_chx_rolls = SkinnyTasteScraper('https://www.skinnytaste.com/buffalo-chicken-egg-rolls/')
    turkey_chili_squash = SkinnyTasteScraper('https://www.skinnytaste.com/turkey-chili-stuffed-acorn-squash/')
    butternut_lasagna = SkinnyTasteScraper('https://www.skinnytaste.com/butternut-squash-and-spinach-lasagna/')
    crab_cakes = SkinnyTasteScraper('https://www.skinnytaste.com/baked-corn-and-crab-cakes/')
    pineapple_fried_rice = SkinnyTasteScraper('https://www.skinnytaste.com/pineapple-shrimp-fried-rice/')
    salmon_burgers = SkinnyTasteScraper('https://www.skinnytaste.com/healthy-salmon-quinoa-burgers/')



    brown_rice.write_to_cvs(csv_writer)
    deviled_eggs.write_to_cvs(csv_writer)
    butternut_mac.write_to_cvs(csv_writer)
    banana_bread.write_to_cvs(csv_writer)
    buff_chx_rolls.write_to_cvs(csv_writer)
    turkey_chili_squash.write_to_cvs(csv_writer)
    butternut_lasagna.write_to_cvs(csv_writer)
    crab_cakes.write_to_cvs(csv_writer)
    pineapple_fried_rice.write_to_cvs(csv_writer)
    salmon_burgers.write_to_cvs(csv_writer)

    print(f'recipes.csv created!')

if __name__ == '__main__':
    main()