   M S B A M L   `   `   `  ����K  GApplicationForBD, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:ApplicationForBDApplicationForBD  5   	   !    ApplicationForBD.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:ApplicationForBD     5   	   _ [MaterialDesignThemes.Wpf, Version=4.5.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   -   5        $	��600��5	   <   $	��476��6I     � WindowStartupLocation$  CenterScreen=�6U    � WindowState$ Normal=�6z    � 
ResizeMode$ NoResize=�6�   
������  � Icon$ images/school_logo.ico��5
   	    � AllowsTransparency .  � WindowStyle$
 None=�6A    � Title 5	   	   ��  �� ElementName$ 	FrameMain�� �� Path$
 Name�.��5      � 	 � RowDefinitions	 5   
   �� 5      $��30 �6   �� 5      $	��240 �6   �� 5      .+�5   
   �� $��3��6   
 �� CornerRadius$
 30��6"   $��0q�64   ��5      �� 5       �� 
StartPoint$ 0.5 0&�6&    �� EndPoint$ 0.5 1&�69   .��5      �  � Color$ #408DD2��6#    � Offset$ 0Q�63   � 5      $ 	#FF25649E��6#   $ 1Q�65   5      5      �� 5   
   -   5        $��
ExitButton��5      $��X��5      $��Right=�5      $��30��5      $��20(�5         
ExitButton#���   5       �� 5!   
   $��1��6   $��Center=�6"   $��0 20 0 0q�6?   .+�5"      �� $��images/school_logo.png��6   $	��160��64   �� 5#      $��школа learn��6   ���#01598E$��30(�5%       �� Padding$ 0q�5&      $��Center=�5'      5)       � 5*   
   -   5          �Name$ 	FrameMain��5*        � NavigationUIVisibility$ Hidden=�6#   $��2��6C   5+      5,      