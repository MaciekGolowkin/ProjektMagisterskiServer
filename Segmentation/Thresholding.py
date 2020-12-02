def Thresholding(orginalImagePath,destinationOfProcessedImaga,x1str,x2str,y1str,y2str):
    import cv2
    import numpy as np

    print("Powinienem Dzialac 2")
    x1=int(x1str);
    x2=int(x2str);
    y1=int(y1str);
    y2=int(y2str);
    print("Powinienem Dzialac 3")
    print(orginalImagePath)
    img = cv2.imread(orginalImagePath,0)
    ret,thresh1 = cv2.threshold(img,80,255,cv2.THRESH_BINARY)
    img[:,:]=0;
    img[x1:x2,y1:y2]=1;
    thresh1=np.multiply(thresh1,img);
    img[:,:]=255;
    img[x1:x2,y1:y2]=0;
    thresh1=thresh1+img;
    cv2.imwrite(destinationOfProcessedImaga,thresh1)

import sys

print("Tutaj sie pojawilem")

Thresholding(sys.argv[2],sys.argv[3],sys.argv[4],sys.argv[5],sys.argv[6],sys.argv[7])
