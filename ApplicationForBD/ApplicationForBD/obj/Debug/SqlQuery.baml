   M S B A M L   `   `   `  ����K  GApplicationForBD, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:ApplicationForBDApplicationForBD  5   	       ApplicationForBD.SqlQuery   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:ApplicationForBD     5   	     � Title$  Ваш SQL-запрос��5   	   $	��450��6    $	��800��6-    � Icon$ images/school_logo.ico��69   $	��450��6W   $	��800��6g    � WindowStartupLocation$ CenterScreen=�6v   .��5	      �  � RowDefinitions 5
   
   �� 5      $��40 �6   �� 5      .+�5   
   ��  �� Orientation$ 
Horizontal=�6   ���	#FFD4D4D4.+�5      �� -   5         ��Name$ 	comboFile��5       �� 
IsEditable .  �� 
IsReadOnly . $	��100��6N   $��Center=�6Z   $��Center=�6u   $��Center=�6�   $��Center=�6�    �� SelectedIndex$ -1��6�   	 �� Text$	 Файл��6�   |�.  .@�5      �� -   5        .��5      �� $��Создать��6    5      �� 5      -   5        .��5      �� $��Открыть��6    5      �� 5      -   5        .��5      �� $��Сохранить��6    5      5      �� 5      -   5        
������ $��0q�5   .   .��5      �� $ 
Horizontal=�6   .+�5      �� $��images/play.png��6   $��25��65   �� 5      $��Выполнить��6    $��Center=�61   $��26(�6L   5      5      5       �� 5!   
   -   5        
 ��Name$
 richTextSQL��5!      $��1��6    �� AcceptsReturn . $ �� HorizontalScrollBarVisibility$
 Auto=�6o   " �� VerticalScrollBarVisibility$
 Auto=�6�   5"      5#      