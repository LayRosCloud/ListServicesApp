   M S B A M L   `   `   `  ����K  GApplicationForBD, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:ApplicationForBD.PagesApplicationForBD.Pages  5      .    (ApplicationForBD.Pages.AdaptiveTablePage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6local$clr-namespace:ApplicationForBD.Pages     5        N� Title$  AdaptiveTablePage��5	      .��5      �  � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      $��25 �6   �� 5      .+�5   
   ��  �� CornerRadius$ 10��6   $��2��6#    J� Opacity$	 0.5Q�60   ��5      �� 5       �� 
StartPoint$	 0 0&�6&    �� EndPoint$ 0.5 1&�67   .��5      �  � Color$ #E55D87��6#    � Offset$ 0Q�63   � 5      $ #5FC3E4��6#   $ 1Q�63   5      5      �� 5   
   -   5         ��Name$ listService��5      ���#003973$
��Bold"�6=   $	 0.8Q�6O   $��2��6]   
������ $
��Auto=�6�   $��Center=�6�   $��0q�6�   6�   �� 5    
   	 �� Orientation$	 
Horizontal=�6   ���	#FF7B97B1.+�5!      �� $��	Sql query��6      
ExitButton#��[  6)   $
��Bold"�6M   5"      �� 5%   
   $	 
Horizontal=�6   $��1��6.   ���	#FF7B97B1.+�5&      �� -   5        #��[  5&      $��Добавить��69   ���#FFF$��70��6^   6i   �� 5'      -   5        
 ��Name$
 
editButton��5'      #��[  6)   $��Изменить��6M   {�.  $	 0.5Q�6r   ���#FFF$��70��6�   6�   �� 5(      -   5        $
 deleteButton��5(      #��[  6+   $��Удалить��6O   {�.  $	 0.5Q�6s   ���#FFF$��70��6�   6�   �� 5)      -   5        $
 refreshButton��5)      #��[  6,   $ ��Перезагрузить��6P   ���#FFF$��80��6z   6�   5*      5+      5,      