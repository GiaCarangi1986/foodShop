   M S B A M L   `   `   `  ����C  ?foodShop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:foodShopfoodShop  5   	       foodShop.BonusCard   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   "localclr-namespace:foodShop  5   	     � Title$  	BonusCard��5	   	   $	��600��6   $	��400��6(    � WindowStartupLocation$ CenterScreen=�65    � WindowStyle$ ThreeDBorderWindow=�6Z   $	��380��5
   	   $	��250��6   .��5      �  � RowDefinitions 5   
   �� 5      $
��0.1* �6   �� 5      $
��0.2* �6   �� 5      $
��0.2* �6   �� 5      $
��0.2* �6    � ColumnDefinitions 5   
   �� 5      $
��0.5* �6   �� 5      $
��0.5* �6   .+�5   
   �� ���	#FF5AE784$��2��6(   �� 5   
   $%��КАРТА ЛОЯЛЬНОСТИ��6   
�������$��Center=�6?   $��Center=�6\   $��22(�5      $��Champagne & Limousines)�6&   $��2��6N   �� 5   
   $��10q�6   $��1��6   $��Center=�6)   $��20(�6D   $��Champagne & Limousines)�5      ��5      �� 5      $
��Wrap=�6    Применить карту:6B   5      �� 5!   
   $��10q�6   $��2��6   $	��Top=�6)   $��20(�6A   $��Champagne & Limousines)�5"      ��5#      �� 5$      $
��Wrap=�6   Списать:6:   5&      �� 5(   
   $��1��6   $
��auto��6    $��10q�6-   $
��Left=�69    �� 
IsEditable .  �� DisplayMemberPath$ number_of_card��5*      $��Center=�6@   $��1��6[   ��5)      �� *
BonusCards+ �� SelectedItem 6.   �� *SelectedBonusCard+ �� UpdateSourceTrigger$ PropertyChanged=�5*   [   � 5,   
   $��2��6   $��1��6!   	 � Orientation$	 Vertical=�61   .+�5-      �� $��10q�6V   ��6   �� *	VvodBonus+$ PropertyChanged=�6V   �� 5/      $��10q�6   $��Center=�6!   $��12(�6<   $��Champagne & Limousines)�50      ��51      �� 52      �� 53      $+�%доступно для снятия=��6"    �� 54      �6"   �� *
MaxBonus+55      57      58      � 5:   
   $	��Top=�6   $��2��6,   $��3��6@   $	 Vertical=�5;      $��Center=�6+   .+�5<      �� $	��150��6   $��10q�6!   $��ПРИМЕНИТЬ��6J   ��6-   �� *
WithCard+6J   �� 5>      $	��150��6   $��10q�6!   $��НЕ ПРИМЕНЯТЬ��6M   ��6-   �� *WithoutCard+6M   �� 5?      $	��150��6   $��10q�6!   $��ПРИОБРЕСТИ��6I   ��6-   �� *	AddCard+6I   5@      5A      5B      