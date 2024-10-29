from PIL import Image, ImageDraw
import math
import os
import random

class PonzoImageMaker:
    width, height = 1200, 900           # 画像サイズ
    lineWidth = 3                       # 線の太さ
    angle = [45,90,135]                 # 角度(°)
    position = [0, 25, 50, 75, 100]     # 位置(%)
    baseLen = [80, 90, 100, 110, 120]   # 基準刺激の長さ
    raito = [80, 90, 100, 110, 120]     # 基準刺激に対する水平線分の大きさ(%)
    convergenceLen = 250                # 輻輳線分の長さ
    conMargin = 5                       # 輻輳線分のマージン
    lieColor = "black"                  # 線の色
    bgColor = "white"                   # 背景色
    dirName = "output"                  # 保存先ディレクトリ名
    
    # コンストラクタ
    def __init__(self):
        self.centerX = self.width/2
        self.centerY = self.height/2
        self.sideAngle = [90 - a/2 for a in self.angle]
        self.main()
        
    # 輻輳線分のマージン計算
    def calMargin(self, ang):
        return ( self.convergenceLen/2 ) * math.cos(math.radians(ang)) + self.conMargin
    
    # 描画処理
    def draw(self, image, draw, m_len, c_pos, ang, rai):
        
        # 基準刺激の描画
        m_left = (self.centerX - m_len) / 2
        draw.line((m_left, self.centerY / 2, m_left+m_len, self.centerY / 2), fill=self.lieColor, width=self.lineWidth)
        
        # 水平線分のパラメータ計算
        c_len = m_len * rai / 100
        c_heigh = self.centerY / 2 + self.centerY
        c_left = self.centerX + (self.centerX - c_len) / 2
        
        # 輻輳線分の描画
        ## 傾きの計算
        con_dy = math.sin(math.radians(ang)) * self.convergenceLen / 2
        con_dx = math.cos(math.radians(ang)) * self.convergenceLen / 2
        con_Ytop = c_heigh - con_dy
        con_Ybottom = c_heigh + con_dy
        ## 中心の計算
        margin = self.calMargin(ang)
        conL_center = c_left - margin
        conR_center = c_left + c_len + margin
        ## 描画
        draw.line((conL_center + con_dx, con_Ytop, conL_center - con_dx, con_Ybottom), fill=self.lieColor, width=self.lineWidth)
        draw.line((conR_center - con_dx, con_Ytop, conR_center + con_dx, con_Ybottom), fill=self.lieColor, width=self.lineWidth)
        
        # 水平線分の描画
        c_heigh = con_Ytop + (con_Ybottom - con_Ytop) * c_pos / 100
        draw.line((c_left, c_heigh, c_left + c_len, c_heigh), fill=self.lieColor, width=self.lineWidth)
        
        return image

    # メイン
    def main(self):
        print("Start.")
        # ディレクトリの作成
        if not os.path.exists(self.dirName):
            print(f"Create directory: {self.dirName}")
            os.makedirs(self.dirName)
        
        for pos in self.position:                               # 水平線分の位置
            for m_len in self.baseLen:                          # 基準刺激の長さ
                for num, ang in enumerate(self.sideAngle):      # 輻輳線分の角度
                    rai = random.choice(self.raito)             # 水平線分の大きさ（ランダムで決定）
                    i = Image.new("RGB", (self.width, self.height), self.bgColor)
                    d = ImageDraw.Draw(i)
                    i = self.draw(i, d, m_len, pos, ang, rai)
                    # i.show()
                    # input("Press Enter to show the next image.") # 表示を一時停止する用
                    i.save(f"{self.dirName}/a{self.angle[num]}_p{pos}_c{m_len}_m{rai}.png")
                    
        
        
        input("Finish. Press Enter to exit.")

PonzoImageMaker()