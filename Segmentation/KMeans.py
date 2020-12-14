
def KMeans(imgPath,imgResult,x1str,x2str,y1str,y2str):
  from sklearn.cluster import KMeans
  import numpy.core.multiarray
  import cv2
  import numpy as np
  import statistics

  x1=int(x1str);
  x2=int(x2str);
  y1=int(y1str);
  y2=int(y2str);
  pic = cv2.imread(imgPath)
  pic_n = pic.reshape(pic.shape[0]*pic.shape[1], pic.shape[2])
  pic_n.shape
  kmeans = KMeans(n_clusters=3, random_state=0).fit(pic_n)
  #r_value, g_value, b_value = int(np.round(kmeans.cluster_centers_[2][0])),int(np.round(kmeans.cluster_centers_[2][1])), np.round((kmeans.cluster_centers_[2][2]))
  xaaa=kmeans.cluster_centers_[0]
  xaaa1=kmeans.cluster_centers_[1]
  xaaa2=kmeans.cluster_centers_[2]


  round_to_tenths = statistics.stdev([int(round(num)) for num in xaaa])
  round_to_tenths1 = statistics.stdev([int(round(num)) for num in xaaa1])
  round_to_tenths2 = statistics.stdev([int(round(num)) for num in xaaa2])

  maksimum=max([round_to_tenths,round_to_tenths1,round_to_tenths2])

  indeks=1;
  if maksimum==round_to_tenths:
    indeks=0
  elif maksimum == round_to_tenths1:
    indeks=1
  else:
    indeks=2


  pic2show = kmeans.cluster_centers_[kmeans.labels_]
  #pic2show = kmeans.cluster_centers_[0]
  cluster_pic = pic2show.reshape(pic.shape[0], pic.shape[1], pic.shape[2])
  cluster_pic[cluster_pic !=  kmeans.cluster_centers_[indeks]] = 255
  cluster_pic[cluster_pic != [255,255,255]] = 0
  cv2.imwrite(imgResult,cluster_pic)

KMeans(sys.argv[2],sys.argv[3],sys.argv[4],sys.argv[5],sys.argv[6],sys.argv[7])