   M S B A M L   `   `   `  ����C  ?foodShop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:foodShopfoodShop  5   	       foodShop.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:foodShop     5   	   b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  Login��5	   	   $	��510��6   $	��300��6$   $	��510��5
   	   $	��250��6    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$ ThreeDBorderWindow=�5   	   $��Segoe UI Emoji)�5   	   .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� $
��auto��6   $	��240��6"   ��5      �� 5       �� EndPoint$ 0.5,1&�6&    �� 
StartPoint$ 0.5,0&�67   .��5      �  � Color$ 	#FFEAFA66��6#   � 5      $ 	#FFB9FF93��6#    � Offset$ 1Q�65   � 5      $ 	#FF8DDCEB��6#   $ 0.546Q�65   5      5      �� 5   
   $	��500��6   $	��350��6#   $��2��63   $
��auto��6D   $��35,194,35,119.6q�6Q    �� RadiusY$ 10��6j   	 �� RadiusX$	 10��6w   
�������$	��150��6�   
 J� Effect
 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         
BlurRadius$ 15Q�6#      	Direction$ 0Q�63      RenderingBias$ Quality=�6A      ShadowDepth$ 1Q�6Y      Color$ 	#FFBBBBBB��6i   5       � 5!   
   $��35,200,35,119.6q�6   $
��auto��6(   $��2��66   $	��350��6G   $	��500��6W   $	��140��6f   .+�5"      �� $��АВТОРИЗАЦИЯ��6   $��Center=�6*   $	��Top=�6G   $��5q�6_   
�������$��17(�6|   � 5#       5$      �� 5%      $��* �6$   �� 5&      $��* �6$   .+�5(      �� $��	5,20,5,10q�6   $��Center=�6-   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ 
ЛОГИН��6H    %  !MaterialDesignFloatingHintTextBox#��[  5)      $��Champagne & Limousines)�6U   $��17(�6}   ��5(   o   �� *Login+ �� UpdateSourceTrigger$ 	LostFocus=� �� Mode$ TwoWay=�5)   }   G� 5*      -   5         G�Name$ PasswordBox��5*   �   $��5,0,5,10q�6   $��Center=�60   $��1��6K   $ ПАРОЛЬ��6X    ) %MaterialDesignFloatingHintPasswordBox#��[ 5+      $��Champagne & Limousines)�6Y   $��17(�6�   5,      5-      �� 5.   
   $��1��6   $	��150��6   $��Center=�6*   $��Bottom=�6G   $
��0,65q�6b   $��
ВОЙТИ��5/   +   ��6   �� *Voiti+��6;   ��  �� ElementName$ PasswordBox���� 50   
   $��1��6   $+��%ПРОДУКТОВЫЙ МАГАЗИН��6!   $��Center=�6<   $��Bottom=�6Y   $��30q�6t   
������� � Cursor$
 Hand��6�   �� 51   
   $��2��6   $��/Image/zakaz.jpg��6!   $
��auto��6<   $��20,20,20,320q�6J   $	��130��6`   $	��200��6p   $	��Top=�6�   52      53      