   M S B A M L   `   `   `  ����M  ISkillProfi.Desktop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:SkillProfi.Desktop.ViewSkillProfi.Desktop.View  5   	   1    +SkillProfi.Desktop.View.CreateRequestWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:SkillProfi.Desktop.View     5   	     � WindowStartupLocation$  CenterScreen=�5   	    � Title$# Создание заявки��5	   	   $	��450��5
   	   $	��500��6    � 
ResizeMode$ NoResize=�5   	   .��5      �� $#��Описание заявки��6   $
��10pxq�5      .��5   
   �  � RowDefinitions 5      �� 5      $��* �6    �� 5      $
��30px �6    .+�5      �� $��0��6    �� Orientation$ Vertical=�6&   .+�5      �� $ 
Horizontal=�6   $��0 5pxq�66   .+�5      �� $��Имя��6    $
��14px(�6+   $��120px��5       �� 5      -   5         ��Name$ tbName��5      $
��14px(�6.   $��330px��5      5      �� 5      $ 
Horizontal=�6   $��0 5pxq�66   .+�5      �� $��Контакты��6    $
��14px(�60   $��120px��5       �� 5       -   5        $ tbEmail��5       $
��14px(�6/   $��330px��5!      5"      �� 5$      $ Vertical=�6   $��0 5pxq�64   $
��Left=�6C   .+�5%      �� $��Текст заявки��6    $
��14px(�64   $��120px��5&       $
��Left=�6.   �� 5'      -   5        $ 	tbMessage��5'      $
��14px(�61   $	��5pxq�5(       �� TextWrapping$
 Wrap=�5)      $��450px��62    �� MaxLines$ 13��5*      5+      5,      �� 5.      $��1��6   $ 
Horizontal=�6&   $��60px 0q�5/      .+�50      �� -   5         ��Name$ btnSave��50      $��Отправить��6*   $��100px��6>   $
��25px��6L   $
��14px(�51      $��
0 0 30px 0q�6)   6=   �� 52      -   5        $��
Выход��52      $��100px��6)   $
��25px��67   $
��14px(�53      6)   54      55      56      58      