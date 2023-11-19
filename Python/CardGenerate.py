from PIL import Image, ImageDraw, ImageFont, ImageOps
import os

x: int = 300
y: int = 450

p: float = 0.8

width: int = int(x * p)
height: int = int(y * p)

dir_in_name = 'input'
dir_out_name = 'output'

def card_draw():
    file_names = os.listdir(dir_in_name)
    
    for in_name in file_names:

        # background
        img = Image.new('RGBA', (x, y), (0, 0, 0, 0))

        d = ImageDraw.Draw(img)
        d.rounded_rectangle((0, 0, x, y), radius=20, outline=(0, 0, 0, 255), width=2, fill=(255,255,255,255))
        d.rounded_rectangle((20, 20, int(x - 20), int(y - 20)), radius=20, outline=(0, 0, 0, 255), width=2, fill=(255,255,255,255))
        
        # image load
        symbol = Image.open(os.path.join(dir_in_name,in_name));
        symbol_fill = Image.new("RGB", symbol.size, (255,255,255,255))
        symbol_fill.paste(symbol, (0,0), symbol)
        symbol = symbol_fill;
        symbol = symbol.resize((width, width))
        
        # image paste
        img.paste(symbol, (int(x * 0.5 - symbol.width * 0.5), int(y * 0.5 - symbol.width * 0.5)))
        
        # image save
        img.save(dir_out_name + "/" + in_name +".png")


def card_bg():
        img = Image.new('RGBA', (x, y), (0, 0, 0, 0))

        d = ImageDraw.Draw(img)
        d.rounded_rectangle((0, 0, x, y), radius=20, outline=(0, 0, 0, 255), width=2, fill=(255,255,255,255))

        img.save(dir_out_name + "/" + "bg" + ".png")
    
if __name__ == '__main__':
    card_draw()
    card_bg()
