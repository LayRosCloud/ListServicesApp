   M S B A M L   `   `   `  ����K  GApplicationForBD, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:ApplicationForBD.PagesApplicationForBD.Pages  5      '    !ApplicationForBD.Pages.TablesPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6local$clr-namespace:ApplicationForBD.Pages     5        N� Title$8  2Админ панель: Список таблиц��5	      .��5      �  � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      .+�5   
   �� -   5         ��Name$ 
listTables��5       $��1��6   
������ $��Disabled=�6M   $��Center=�6�   $��0q�6�   ��5      �� 5      .R�5      �  � Orientation$ 
Horizontal=�6    $��Center=�69   5      ��5      �� 5      .R�5      �� $	��245��6!   $	��200��6-   $��Center=�6:   $��5q�6U   $��Center=�6`   .+�5      �� $��Имя таблицы��6$   ���#FFF$��Center=�6I    J� Opacity$	 0.8Q�6f   �� 5      ���#FFF$��25(�6Q   $��Center=�6_   $
��Wrap=�6|   ��6$   �� *	NameTable+6|   �� 5      $"��время создания:��6$   ���#FFF$��Center=�6M   $	 0.5Q�6j   �� 5      ���#FFF$��20(�6R   $��Center=�6`   $	 0.6Q�6}   ��6$   �� *
CreateTime+6}   �� 5      $.��(время редактирования:��6$   ���#FFF$��Center=�6S   $	 0.5Q�6p   �� 5      ���#FFF$��20(�6T   $��Center=�6b   $	 0.6Q�6   ��6$   �� *RedactorTime+6   5       5!      5#      �� 5'   
   $	��300��6   $
��Left=�6    �� CornerRadius$ 10��68   ��5(      �� 5)       �� Color$
 #FFF��6"   5+      �� 5,   
    �� Orientation$ 
Horizontal=�6   .+�5-      �� -   5         ��Name$ findTextBox��5-      $	��300��5.      
�������$��Поиск...��50      
������ $��15(�52      $��Center=�53      $��0q�54      56      �� 59      -   5        $#��Клиенты сегодня��59         
ExitButton#��[  5:      $��0q�5;      ���	#FFFCFFDC$��5 0 0 0q�5=      5>      5?      5@      5A      