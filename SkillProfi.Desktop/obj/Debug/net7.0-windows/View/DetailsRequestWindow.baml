   M S B A M L   `   `   `  ����M  ISkillProfi.Desktop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:SkillProfi.Desktop.ViewSkillProfi.Desktop.View  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	   I2clr-namespace:System.Collections;assembly=mscorlibSystem.Collections 5   	   2    ,SkillProfi.Desktop.View.DetailsRequestWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:SkillProfi.Desktop.View     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	   Bcol2clr-namespace:System.Collections;assembly=mscorlib     5   	     � WindowStartupLocation$  CenterScreen=�5
   	    � Title$# Описание заявки��5   	   $	��450��5   	   $	��400��6    � 	Resources 5      U QSystem.Runtime, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"  System.Collections.ArrayList   statuses��%�   &	          5   
   ��5      Получена��6#   ��5      В работе��6#   ��5      Выполнена��6$   ��5      Отклонена��6$   ��5      Отменена��6#   5      5      .��5      �� $#��Описание заявки��6   $
��10pxq�5      .��5   
   �  � RowDefinitions 5      �� 5      $��* �6    �� 5      $
��30px �6    .+�5      �� $��0��6    �� Orientation$ Vertical=�6&   .+�5       �� $ 
Horizontal=�6   $��0 5pxq�66   .+�5!      �� $��Id��6    $
��14px(�6*   $��120px��5"       �� 5#      -   5         ��Name$
 tbId��5#       $
��14px(�6.   5$      �� 5&      $ 
Horizontal=�6   $��0 5pxq�66   .+�5'      �� $#��Заявка получена��6    $
��14px(�67   $��120px��5(       �� 5)      -   5        $ 	tbCreated��5)       $
��14px(�63   5*      �� 5,      $ 
Horizontal=�6   $��0 5pxq�66   .+�5-      �� $��Имя��6    $
��14px(�6+   $��120px��5.       �� 5/      -   5        $ tbName��5/       $
��14px(�60   50      �� 52      $ 
Horizontal=�6   $��0 5pxq�66   .+�53      �� $��Текст заявки��6    $
��14px(�64   $��120px��54       �� 55      -   5        $ 	tbMessage��55       $
��14px(�63   $
��Wrap=�56       $��230px��64   57      �� 59      $ 
Horizontal=�6   $��0 5pxq�66   .+�5:      �� $��Контакты��6    $
��14px(�60   $��120px��5;       �� 5<      -   5        $ tbEmail��5<       $
��14px(�61   5=      �� 5?      $ 
Horizontal=�6   $��0 5pxq�66   .+�5@      �� $��Статус��6    $
��14px(�6.   $��120px��5A       �� 5B      -   5         ��Name$ cbStatus��5B      $
��14px(�61   #��[  5C      5D      5E      �� 5G      $��1��6   $ 
Horizontal=�6&   $��60px 0q�5H      .+�5I      �� -   5         ��Name$ btnSave��5I      $��Сохранить��6*   $��100px��6>   $
��25px��6L   $
��14px(�5J      $��
0 0 30px 0q�6)   6=   �� 5K      -   5        $��
Выход��5K      $��100px��6)   $
��25px��67   $
��14px(�5L      6)   5M      5N      5O      5Q      