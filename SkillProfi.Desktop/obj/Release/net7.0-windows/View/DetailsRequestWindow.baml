   M S B A M L   `   `   `  ���� M  ISkillProfi.Desktop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:SkillProfi.Desktop.ViewSkillProfi.Desktop.View  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem I2clr-namespace:System.Collections;assembly=mscorlibSystem.Collections 2    ,SkillProfi.Desktop.View.DetailsRequestWindow   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     7local%clr-namespace:SkillProfi.Desktop.View     6sys&clr-namespace:System;assembly=mscorlib     Bcol2clr-namespace:System.Collections;assembly=mscorlib       � WindowStartupLocation$  CenterScreen=� � Title$# Описание заявки��$	��550��$	��500�� � 	Resources U QSystem.Runtime, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"  System.Collections.ArrayList   statuses��%�   &	          ��Получена����В работе����Выполнена����Отклонена����Отменена��.���� $#��Описание заявки��$
��10pxq�.���  � RowDefinitions �� $��* ��� $
��30px �.+��� $��0�� �� Orientation$ Vertical=�.+��� $ 
Horizontal=�$��0 5pxq�.+��� $��Id��$
��14px(�$��120px���� -    ��Name$
 tbId��$
��14px(��� $ 
Horizontal=�$��0 5pxq�.+��� $#��Заявка получена��$
��14px(�$��120px���� -   $ 	tbCreated��$
��14px(��� $ 
Horizontal=�$��0 5pxq�.+��� $��Имя��$
��14px(�$��120px���� -   $ tbName��$
��14px(��� $ 
Horizontal=�$��0 5pxq�.+��� $��Текст заявки��$
��14px(�$��120px���� -   $ 	tbMessage��$
��14px(�$
��Wrap=�$��230px���� $ 
Horizontal=�$��0 5pxq�.+��� $��Контакты��$
��14px(�$��120px���� -   $ tbEmail��$
��14px(��� $ 
Horizontal=�$��0 5pxq�.+��� $��Статус��$
��14px(�$��120px���� -    ��Name$ cbStatus��$
��14px(�#��[  �� $��1��$ 
Horizontal=�$��60px 0q�.+��� -    ��Name$ btnSave��$��Сохранить��$��100px��$
��25px��$
��14px(�$��
0 0 30px 0q��� -   $��
Выход��$��100px��$
��25px��$
��14px(�