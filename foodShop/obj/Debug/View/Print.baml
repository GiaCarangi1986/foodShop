   M S B A M L   `   `   `  ����C  ?foodShop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:foodShopfoodShop  5   	       foodShop.Print   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:foodShop     5   	     � Title$  Print��5   	   $	��800��6   $	��800��6$    � WindowStartupLocation$ CenterScreen=�61    � WindowStyle$ ThreeDBorderWindow=�6V   $	��500��5	   	   $	��500��6   .��5
      �  � RowDefinitions 5   
   �� 5      $
��0.1* �6   �� 5      $��* �6   �� 5      $
��0.1* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $
��0.8* �6   .+�5   
   �� ���	#FF5AE784$��3��6(   �� 5   
   $&�� ВЕРСИЯ ДЛЯ ПЕЧАТИ��6   
�������$��Center=�6@   $��Center=�6]   $��22(�5      $��Champagne & Limousines)�6&   $��3��6N   �� 5   
   $��1��6   ��6   �� *Line_of_checks+��5      �� 5      .R�5      �� $��5q�6!   .+�5      �� $��18(�6$   ��62   ��  �� Path$ name_of_product��� 5      �� 5   "   $�
цена: ��6&    �� 5   "   �6&   �� $ cost_for_buyer� �� StringFormat$
 0.00��5      �� 5       �� 5!   "   $�кол-во: ��6&    �� 5"   "   �6&   �� $ much_of_products�5#      5$      5%      5'      �� 5(   
   $
��auto��6   $��10q�6   $��2��6*   $	��Top=�67   $
��Left=�6O   $
��Ок��5)      ��5(   j   �� *Ok+5)      � 5+   
   $��1��6   $��1��6    5,      �� 5-      $
��0.1* �6    �� 5.      $
��0.1* �6    �� 5/      $
��0.1* �6    �� 50      $
��0.1* �6    �� 51      $
��0.1* �6     53      �� 54      $��* �6#   �� 55      $��* �6#   .+�57      �� $��10q�6   $��16(�6    $��Center=�6.   $��Champagne & Limousines)�58      ��59      �� 5:      $
��Wrap=�6    Сумма чека, руб:6F   5<      �� 5>      $��10q�6   $��16(�6    $��1��6.   $��Center=�6;   $��Champagne & Limousines)�5?      y�5>   V   �� *Vis+��5A      �� 5B      $
��Wrap=�6   *(Скидка составила, руб:6H   5E      �� 5G      $��10q�6   $��16(�6    $��2��6.   $��Center=�6;   $��Champagne & Limousines)�5H      y�5G   V   �� *Vis+��5I      �� 5J      $
��Wrap=�6   B@Итоговая стоимость со скидкой, руб:6U   5L      �� 5N      $��10q�6   $��16(�6    $��3��6.   $��Center=�6;   $��Champagne & Limousines)�5O      y�5N   V   �� *Vis+��5P      �� 5Q      $
��Wrap=�6   !Бонусов на карте:6C   5S      �� 5U   
   $��1��6   $��10q�6    $��17(�6,   $��Champagne & Limousines)�6:   $
��Left=�5V   @   $��Center=�6[   ��6   �� $	 Sum�$
 0.00��6[   �� 5X      $��1��6   $��1��6!   $��10q�61   $��17(�6=   $��Champagne & Limousines)�6K   $
��Left=�5Y   A   $��Center=�6\   y�5X   s   �� *Vis+��5Y      �� $
 Sale�$
 0.00��6\   �� 5[      $��2��6   $��1��6!   $��10q�61   $��17(�6=   $��Champagne & Limousines)�6K   $
��Left=�5\   A   $��Center=�6\   y�5[   s   �� *Vis+��5\      �� $
 Itog�$
 0.00��6\   �� 5^      $��3��6   $��1��6!   $��10q�61   $��17(�6=   $��Champagne & Limousines)�6K   $
��Left=�5_   G   $��Center=�6b   y�5^   s   �� *Vis+��5_      �� $ 
NowBonusov�$
 0.00��6b   5`      5a      5b      