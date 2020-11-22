function [outputArg1] = WyciecieSzarosci()
img=imread('C:\Users\Maciek\Desktop\PracaMagisterska\IMG_20201115_013015__01.jpg');
redChannel = double(img(:,:,1));
greenChannel = double(img(:,:,2));
blueChannel = double(img(:,:,3));
srednia=(redChannel+greenChannel+blueChannel)/3;
macierz=(((redChannel-srednia).^2)+((blueChannel-srednia).^2)+((greenChannel-srednia).^2))/3;
odchylenie=sqrt(macierz);
blackpixelsmask = odchylenie<16;
newImage=rgb2gray(img);
BW = im2bw(newImage);
% imshowpair(img,blackpixelsmask,'montage')
imwrite(blackpixelsmask,"Test.png")
end

