   M S B A M L   `   `   `  ����U  QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	   M ITakeADrink, Version=2017.5.27.19037, Culture=neutral, PublicKeyToken=null'clr-namespace:TakeADrink
TakeADrink 5   	      TakeADrink.MainWindow   5      HControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro  5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:TakeADrink     5   	   -   5          � Title$  
MainWindow��5	   	   $	��595��6   $	��790��6)   (   "MahApps.Metro.Controls.MetroWindow   ShowIconOnTitleBar .    IconTemplate 5      �� 5   
   .R�5      �� $��20��6   $��20��5      ��5      ��  �� RelativeSource ��  �� AncestorType$ ButtonL� �� Path$ 
Foreground� J� OpacityMask 5      !� 5       j� Stretch$
 Fill=�6"      appbar_cloud !� Visual# [  61   5      5      .��5      � .+�5   
   �� -   5        	 ��Name$	 label1��5      $��	Location:��6    $
��Left=�64   $��	10,10,0,0q�6O   $	��Top=�6b   �� 5   
   -   5        $	 label2��5      $�� ��6    $
��Left=�6+   $��	73,10,0,0q�6F   $	��Top=�6Y   �� 5   
   -   5        
 ��Name$
 	StatusBar��5      $��Stretch=�6'   $��35��6E   $��Bottom=�6Q   .@�5      �� -   5        $	 label_Status��5      $�� ��6*   5      �� 5   
   -   5        $	 label��5      $��Temperature:��6   $
��Left=�66   $��	10,62,0,0q�6Q   $	��Top=�6d   �� 5   
   -   5        $	 label3��5      $��	Humidity:��6    $
��Left=�64   $��	10,88,0,0q�6O   $	��Top=�6b    J� RenderTransformOrigin$ 0.151,-3.543&�6z   �� 5   
   -   5        $	 label4��5      $��Air Pressure:��6    $
��Left=�68   $��
10,114,0,0q�6S   $	��Top=�6g   $ 0.5,0.5&�6   �� 5   
   -	   5        $	 label5��5      $��Weather:��6    $
��Left=�63   $��	10,36,0,0q�6N   $	��Top=�6a   �� 5    
   -
   5        $	 label6��5       $��Wind:��6    $
��Left=�60   $��
10,140,0,0q�6K   $	��Top=�6_   $ 0.5,0.5&�6w   �� 5!   
   -   5        $	 label7��5!      $�� ��6    $
��Left=�6+   $��	94,10,0,0q�6F   $	��Top=�6Y   �� 5"   
   -   5        $	 label8��5"      $�� ��6    $
��Left=�6+   $��	94,36,0,0q�6F   $	��Top=�6Y   �� 5#   
   -   5        $	 label9��5#      $�� ��6    $
��Left=�6+   $��	94,62,0,0q�6F   $	��Top=�6Y   �� 5$   
   -   5        $	 label10��5$      $�� ��6!   $
��Left=�6,   $��	94,88,0,0q�6G   $	��Top=�6Z   �� 5%   
   -   5        $	 label11��5%      $�� ��6!   $
��Left=�6,   $��
94,114,0,0q�6G   $	��Top=�6[   �� 5&   
   -   5        $	 label12��5&      $�� ��6!   $
��Left=�6,   $��
94,140,0,0q�6G   $	��Top=�6[   5)         RightWindowCommands 5*      +   %MahApps.Metro.Controls.WindowCommands  5+   
   .@�5,      �� .��5-      �� -   5         ��Name$ SP_Settings��5-       �� Orientation$ 
Horizontal=�62   .+�5.      �� $��20��6    $��20��5/      ��50      ��  �� $ ButtonL�$ 
Foreground� 51      !� 52      $
 Fill=�6*     appbar_cogs# [ 69   54      �� 55      $��4 0 0 0q�6    $��Center=�56      $��Settings��57      58      59      �� 5:      .��5;      �� -   5        $ SP_Contribute��5;      $ 
Horizontal=�64   .+�5<      �� $��20��6    $��20��5=      ��5>      ��  �� $ ButtonL�$ 
Foreground� 5?      !� 5@      $
 Fill=�6*     appbar_github# [ 69   5B      �� 5C      $��4 0 0 0q�6    $��Center=�5D      $��
Contribute��5E      5F      5G      �� 5H      .��5I      �� -   5        $ SP_About��5I      $ 
Horizontal=�6/   .+�5J      �� $��20��6    $��20��5K      ��5L      ��  �� $ ButtonL�$ 
Foreground� 5M      !� 5N      $
 Fill=�6*     appbar_information_circle# [ 69   5P      �� 5Q      $��4 0 0 0q�6    $��Center=�5R      $��About��5S      5T      5U      5V      5X      