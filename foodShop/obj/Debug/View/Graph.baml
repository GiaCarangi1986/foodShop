   M S B A M L   `   `   `  ����C  ?foodShop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:foodShopfoodShop  5   	   U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf 5	   	       foodShop.Graph   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:foodShop     5   	   b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   >lvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf  5	   	   $	��620��5
   	   $
��1080��6     � WindowStartupLocation$  CenterScreen=�6$    � WindowStyle$ ThreeDBorderWindow=�6I   $	��620��5   	   $	��300��6    � Title$ Graph��6(   .��5      �  � RowDefinitions 5   
   �� 5      $
��0.1* �6   �� 5      $
��0.9* �6   .+�5   
   �� ���	#FF5AE7846   �� 5   
   $��ГРАФИК��6   
�������$��Center=�65   $��Center=�6R   $��22(�5      $��Champagne & Limousines)�6"   � 5   
   $��1��6    5      �� 5      $
��0.8* �6    �� 5      $
��0.2* �6    .+�5      #  MaterialDesignThemes.Wpf.Card  +  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth0=�5      $��32q�5      $
��auto��6   $	��440��6    .��5      #  LiveCharts.Wpf.CartesianChart  &   LiveCharts.Wpf.Charts.Base.Chart   LegendLocation$ Right=�6I   $��0,0,-0.4,-20.2q�6`      Series 6%   �� *SeriesCollection+   AxisX 5        LiveCharts.Wpf.Axis     Title$ дата��6#   	   Labels	 60   �� *Labels+
   AxisY
 5!        $ руб��6#      LabelFormatter 6/   �� *
yFornatter+5#      5$      � 5%      $��2��6    � Orientation$ Vertical=�6%   $��20,0,0,0q�6<   .+�5&      �� $��10q�6   $��16(�6$   $��Champagne & Limousines)�5'      ��5(      �� 5)      �� 5*      $*�$Всего оплачено, руб=��6"    �� 5+      �6"   �� *Sum+ �� StringFormat$
 0.00��5,      5.      �� 5/      $��10q�6   $��16(�6$   $��Champagne & Limousines)�50      ��51      �� 52      �� 53      $0�*Бонусами оплачено, руб=��6"    �� 54      �6"   �� *Bon+$
 0.00��55      57      58      59      5:      5;      