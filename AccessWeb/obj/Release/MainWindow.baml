   M S B A M L   `   `   `  ���� D  @AccessWeb, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:AccessWeb	AccessWeb      AccessWeb.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:AccessWeb       � Title$  自动访问网页��$
��1200��$	��600�� � Icon$ AccessWeb.ico��.���  � RowDefinitions �� �� $	��146 � � ColumnDefinitions �� $	��427 ��� $��* �.+��� -    ��Name$ button_deleteAll��$��全部删除��$
��Left=�$��	61,13,0,0q�$	��Top=�$��75��$��22��$��1��$��Center=�$��Center=��� -   $ button_delete��$��删除��$
��Left=�$��
165,13,0,0q�$	��Top=�$��75��$��22��$��1��$��Center=�$��Center=��� -   $ button_Import��$��导入��$
��Left=�$��
257,13,0,0q�$	��Top=�$��75��$��22��$��1��$��Center=�$��Center=��� -   $ button_stop��$��停止��$��Right=�$��
0,79,250,0q�$��1��$��75��$��50��$	��Top=��� -   $ button_importTXT��$��	导入TXT��$��
350,13,0,0q�$	��Top=�$��22�� J� RenderTransformOrigin$ 0.191,0.536&�$
��Left=�$��75��$��1��$��Center=�$��Center=��� -   $ 
button_run��$��运行��$
��Left=�$��7,79,0,0q�$	��Top=�$��50��$��1��$��75���� -    ��Name$ listView_domain��$��
0,24,187,0q� �� ItemBindingGroup &   System.Windows.Data.BindingGroup   �� View �� .P��� $��编号��	 �� Width$	 40��
 �� DisplayMemberBinding
 ��  �� Path$ ID� �� Mode$ OneWay=��� $��域名��$		 130��
 �� $
 Name�$ OneWay=��� $��状态��$	 70��
 �� $ Status�$ OneWay=� � ContextMenu �� �� -    ��Name$ richTextBox_domain��$��
239,24,0,0q�$ �� HorizontalScrollBarVisibility$
 Auto=�" �� VerticalScrollBarVisibility$
 Auto=�. �/�  /� 
LineHeight$ 1��.T�J� .*�.*��� $� ���� -	   $ richTextBox_log��$	��342��$��0,0,0,1q�$��Bottom=�$��2��$��1��$
 Auto=�$
 Auto=�. �/� $ 1��(  "System.Windows.Controls.DatePicker  -
     Name$ DatePicker_interval��$
��Left=�$��
335,62,0,0q�$��1��$	��Top=�$��24��$��82��$ 0.695,-0.75&�   IsTodayHighlighted .  �� -    ��Name$ 	textBlock��$
��Left=�$��0,3,0,0q�$
��Wrap=�$!��已添加的域名列表：��$	��Top=�$��16��$	��108���� -   $ 
textBlock1��$
��Left=�$��	239,3,0,0q�$
��Wrap=�$��要导入的域名：��$	��Top=�$��16��$��84���� -   $ 
textBlock2��$
��Left=�$��
203,46,0,0q�$��1��$
��Wrap=�$'��!域名单个访问时间设置：��$	��Top=�$��16��$	��132���� -   $ 
textBlock3��$
��Left=�$��
201,91,0,0q�$��1��$
��Wrap=�$	��Top=�$ 0.745,-0.688&�$��16��$	��158���� $-�'所有域名更新二次访问设置：���� ��   -   $ DatePicker_allInterval��$
��Left=�$��335,117,0,0q�$��1��$	��Top=� .  �� -    ��Name$ comboBox��$
��Left=�$��	57,46,0,0q�$��1��$	��Top=�$	��120�� �� 
IsEditable . 