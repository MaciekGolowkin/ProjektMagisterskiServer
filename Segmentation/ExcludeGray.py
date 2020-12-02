import numpy.core.multiarray
import cv2
import numpy as np

def ExcludeGray(pathOfImage,destinationPath,x1,x2,y1,y2):
    img = cv2.imread(pathOfImage)  

    redChannel =img[:,:,2].astype(np.float)
    greenChannel = img[:,:,1].astype(np.float)
    blueChannel = img[:,:,0].astype(np.float)
    
    srednia=(redChannel+greenChannel+blueChannel)/3;
    macierz=((np.power((redChannel-srednia),2))+(np.power((greenChannel-srednia),2))+(np.power((blueChannel-srednia),2)))/3;
    odchylenie=np.sqrt(macierz);

    blackpixelsmask = odchylenie<20;

    helperIMG=odchylenie;
    helperIMG[:,:]=0;
    helperIMG[x1:x2,y1:y2]=1;

    newImage=blackpixelsmask.astype(np.float)*255

    finalImage=np.multiply(newImage,helperIMG);
    matrixToAdd=odchylenie;
    matrixToAdd[:,:]=255;
    matrixToAdd[x1:x2,y1:y2]=0;

    wynik=finalImage+matrixToAdd;

    cv2.imwrite(destinationPath,wynik)

import sys

ExcludeGray(sys.argv[2],sys.argv[3],sys.argv[4],sys.argv[5],sys.argv[6],sys.argv[7])



