   M S B A M L   `   `   `  ����K  GApplicationForBD, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:ApplicationForBDApplicationForBD  5   	        ApplicationForBD.HubWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:ApplicationForBD     5   	     � Title$  Главное окно��5   	   $	��450��6   $	��800��6.   $
��1280��6=   $	��720��6J    � WindowStyle$
 None=�6W    � 
ResizeMode$ CanMinimize=�6j    � WindowStartupLocation$ CenterOwner=�6�    � Icon$ images/school_logo.ico��5	   	   
������  � AllowsTransparency . .��5
      �  � ColumnDefinitions 5   
   �� 5      $	��300 �6   �� 5      .+�5   
   ��  �� CornerRadius$ 10��6   $��2��5      ��5      �� 5       �� 
StartPoint$ 0.5 0&�6&   	 �� EndPoint$	 0.5 1&�69   .��5      � 
 � Color$
 #009FFD��6#    � Offset$ 0Q�63   � 5      $
 #2A2A72��6#   $ 1Q�63   5      5      � 5   
   $��1��6    � RowDefinitions 5      �� 5      $��30 �6    �� 5      $��1* �6    .+�5#      �� -   5        
������ 5#   +   �� 5&      $��Right=�6    �� Orientation$ 
Horizontal=�65   .+�5'      �� -   5        $��
HideButton��5'   =      
ExitButton#��[  6   $��_��6O   $��30��6[   $��0q�6f   
������ ���#FFF$
��Bold"�6�   6�   �� 5(      -   5        $��UpScaleButton��5(   =   #��[  6   $	��[ ]��6R   $��30��6`   $��0q�6k   
������ ���#FFF$
��Bold"�6�   6�   �� 5)      -   5        $��
ExitButton��5)   =   #��[  6   $��X��6O   $��30��6[   $��0q�6f   
������ ���#FFF$
��Bold"�6�   6�   5*       � 5-      -   5          �Name$ HubFrame��5-      $��1��6&     � NavigationUIVisibility$ Hidden=�63   5.      �� 50   
   $ 10��6   ��51      �� 52      $ 0.5 0&�6&   $	 0.5 1&�69   .��53      � $
 #0652C5��6#   $	 0.2Q�63   � 54      $
 #D4418E��6#   $ 1Q�63   55      57      � 58   
    59      �� 5:      $��50 �6    �� 5;      $	��120 �6    �� 5<      $��1* �6    .+�5A      �� $ 
Horizontal=�6   $��Center=�62   .+�5B      �� $��images/school_logo.png��6   �� 5C      $��5 0 0 0q�6   .+�5D      �� 
�������$��школа learn��5E       $��20(�5F       �� 5G      
�������$��меню��5H       $��15(�5I       $��	0 -10 0 0q�5J        J� Opacity$	 0.7Q�5K       5L      5M      �� 5P      -   5        $��Center=�5P      $��Right=�5Q      $��0q�5R      
������ .��5U      � .+�5V      �� $��0��6   $��0��5W      $��/images/notifications.png��5X      $��38��5Y      $��Right=�5Z      �� 5[      -   5         ��Name$ notificationCountBorder��5[      $��25��5\      $��25��5]      $��Bottom=�5^      $��Right=�5_      $ 25��5`      $y�	Collapsed=�5a      ��5b      �� 5c       �� Color$ 	#FFFFAD1A��6.   ._�5e      �� -   5         ��Name$ CountNotificies��5e   $   $��Center=�6=   $��Center=�6X   $��0��6u   ���#FFF$��10(�6�   5f      5g      5h      �� 5k      ���#14557B$��1��6*   $��0 5 0 0q�67   �� 5l      -	   5        #��[  5l      $��1��69   .��5m      �� $��Center=�6   $��Center=�68   $ 
Horizontal=�6U   $��1��6n   .+�5n      �� $��50��6   $��50��6(   $ 10��64   ��5o      �� 5p      $	 0 0&�62   $		 1 1&�6C   .��5q   "   � $
 #6D17CB��6/   $ 0Q�6?   � 5r   "   $
 #2876F9��6/   $ 1Q�6?   5s      ._�5u      �� -
   5        $ reductionText��5u   $   $��AB��6;   $��Center=�6E   $��Center=�6`   $��25(�6}   ���#FFF6�   5v      �� 5w      $ Vertical=�6!   $��5 0 0 0q�68   $��Center=�6I   .+�5x      �� -   5        $ nameUserText��5x   $   ���#FFF$%��Имя пользователя��6L   �� 5y      -   5        $ emailUserText��5y   $   ���#FFF$)��#Почта пользователя��6M   $	 0.8Q�6g   $��0 -5 0 0q�6u   �� 5z      -   5        $ lvlUserText��5z   $   ���#FFF$6��0Уровень прав пользователя��6K   $	 0.7Q�6l   $��0 -5 0 0q�6z   5{      5|      5}      �� 5�      $ Vertical=�6   $��2��60   .+�5�      �� -   5        $��ProfileButton��5�      $��0q�6.   $��0q�6:   $��0q�6E   
������ #��[  6r    � Cursor$
 Hand��6�   ��5�      �� 5�      $ 
Horizontal=�6%   $	 0.8Q�6>   $��0q�6L   $	��300��6W   $��50��6c   .+�5�      �� ���#fff$��5��64   �� 5�      $��images/account.png��6$   $��35��6@   $��60 0 0 0q�6K   �� 5�      $��Профиль��6(   $��Center=�67   ���#fff$��25(�6d   5�      5�      �� 5�      -   5        $��listButtonService��5�      $��0q�62   $��0q�6>   $��0q�6I   
������ #��[  6v   $
 Hand��6�   ��5�      �� 5�      $ 
Horizontal=�6%   $	 0.8Q�6>   $��0q�6L   $	��300��6W   $��50��6c   .+�5�      �� ���#fff$��5��64   �� 5�      $��images/listServices.png��6$   $��35��6E   $��60 0 0 0q�6P   �� 5�      $��Список услуг��6(   $��Center=�6<   ���#fff$��25(�6i   5�      5�      �� 5�      -   5         ��Name$ AdminButton��5�      $��0q�6.   $��0q�6:   $��0q�6E   
������ #��[  6r   $y�	Collapsed=�6�   ��5�      �� 5�      $ 
Horizontal=�6%   $	 0.8Q�6>   $��0q�6L   $	��300��6W   $��50��6c   .+�5�      �� ���#fff$��5��64   �� 5�      $ ��images/adminPanelImage.png��6$   $��35��6H   $��60 0 0 0q�6S   �� 5�      $��Админ панель��6(   $��Center=�6<   ���#fff$��25(�6i   5�      5�      �� 5�      $��0q�6   $��0 0 0 0q�6%   $��0q�66   
������ #��[  6c   $��Bottom=�6�   $��2��6�   ��5�      �� 5�      $ 
Horizontal=�6%   $	 0.8Q�6>   $��0q�6L   $	��300��6W   $��50��6c   .+�5�      �� ���#fff$��5��64   �� 5�      $��images/settings.png��6$   $��35��6A   $��60 0 0 0q�6L   �� 5�      $��Настройки��6(   $��Center=�69   ���#fff$��25(�6f   5�      5�      5�      �� 5�      -   5        $��0q�5�      $��0 0 0 10q�6!   $��0q�63   
������ #��[  6`   $��2��6�   $��Bottom=�6�   ��5�      �� 5�      $ 
Horizontal=�6!   $	 0.6Q�6:   $��0q�6H   $	��300��6S   $��50��6_   .+�5�      �� ���#fff$��5��60   �� 5�      $!��images/exitForLoginPage.png��6    $��35��6E   $��30 0 0 0q�6P   �� 5�      $��
Выйти��6$   $��Center=�61   ���#fff$��25(�6^   $��10 0 0 0q�6l   5�      5�      5�      �� 5�   
   -   5        $ SizeWE��5�      
������ $��1��67   $��7��6G   $��Right=�6Q   $��3��6m   6�   �� 5�   
   -   5        $ SizeNS��5�      
������ $��2��67   $��Bottom=�6K   $��7��6f   6�   5�      5�      