   M S B A M L   `   `   `  ����C  ?foodShop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:foodShopfoodShop  5   	       foodShop.AddCheck   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:foodShop     5   	   b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  AddCheck��5	   	   $	��800��5
   	   $	��800��6    � WindowStartupLocation$ CenterScreen=�6#    � WindowStyle$ ThreeDBorderWindow=�6H   $	��500��5   	   $	��500��6   .��5      �  � RowDefinitions 5   
   �� 5      $
��0.1* �6   �� 5      $��* �6   �� 5      $
��0.1* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $
��0.8* �6   .+�5   
   �� ���	#FF5AE784$��3��6(   �� 5   
   $��ЧЕК��6   
�������$��Center=�62   $��Center=�6O   $��22(�5      $��Champagne & Limousines)�6&   $��3��6N   �� 5   
   $��1��6   ��6   �� *Line_of_checks+��5      �� 5      .R�5      �� $��5q�6!   .+�5      �� $��18(�6$   ��62   ��  �� Path$ name_of_product��� 5      �� 5    "   $�
цена: ��6&    �� 5!   "   �6&   �� $ cost_for_buyer� �� StringFormat$
 0.00��5"      �� 5#      �� 5$   "   $�кол-во: ��6&    �� 5%   "   �6&   �� $ much_of_products�5&      5'      5(      5*      � 5,   
   $��1��6   $��1��6!   $��10q�61    � Orientation$ Vertical=�6=   .+�5-      �� $��10q�6   $��20(�6    $��Champagne & Limousines)�5.      ��5/      �� 50      $
��Wrap=�6    $"Выберите название:6H   52      �� 54      -   5         ��Name$ nameProduct��54   0   $
��auto��6   $��10q�6$   	 �� 
IsEditable	 . 
 �� DisplayMemberPath$
 title��56      $ �� IsSynchronizedWithCurrentItem$
 TrueY�65   ��54   E   �� *
Products+ �� SelectedItem 55   -   �� *SelectedProduct+ �� UpdateSourceTrigger$ PropertyChanged=�56   5   �� 58      $��10q�6   $��16(�6    $��Champagne & Limousines)�59      ��5:      �� 5;      �� 5<      $�	цена=��6"    �� 5=      �6"   �� *Price+5>      5@      �� 5B      $��10q�6   $��20(�6    $��Champagne & Limousines)�5C      ��5D      �� 5E      $
��Wrap=�6    ;9Выберите количество продуктов:6T   5G      �� 5I      ��6   �� *	VvodMax+$ PropertyChanged=��� 5K      $��10q�6   $��16(�6    $��Champagne & Limousines)�5L      ��5M      �� 5N      �� 5O      $�	макс=��6"    �� 5P      �6"   �� *Max+5Q      5S      �� 5U      $0��*Добавить новый продукт��5V      $
��auto��66   $��10q�6C   ��5U      �� *AddLine_of_check+$ PropertyChanged=�5V   C   5W      � 5Y   
   $��2��6   $	��Top=�6!   $
��Left=�69   .+�5Z      �� $
��auto��6   $��10q�6"   $��Составить чек��5[      ��5Z   /   �� *
GetCheck+5[      5]      5_      5`      