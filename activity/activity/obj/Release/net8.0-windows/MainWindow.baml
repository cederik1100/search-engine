   M S B A M L   `   `   `  ���� C  ?activity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:activityactivity      activity.MainWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     (localclr-namespace:activity       � Title$  
MainWindow��$	��570��$
��1015�� � WindowStartupLocation$ CenterScreen=� � 	Resources  �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius��%  '��       �� " ��.���  � �� �� " ��.R��� #��z��$ 10��
���   �$��1q�._��� $��Center=�$��Center=���  � ��$ 5q���  � �� �#6EACDA��  � ��
 �������  � ��$
 Bold"���  � ��$ 80����  � ��$ 30��.��� ���#E2E2B6$��0,0,-11,-51q� � RowDefinitions �� $	��100 ��� $	��400 �	 � ColumnDefinitions	 �� $	��100 ��� $	��800 ��� $	��100 �.+��� $��3�����#03346E�� $
��Left=�$��64��$��14,0,0,0q�$��Center=�$��65��$��/search.png���� -   $��TBSearch��$
��Left=�$��	230,0,0,0q�
 �� TextWrapping$

 Wrap=�$��Center=�$	��366��$��30��$��1��.������ " ��.���  � �� �� " ��.R��� $ 10��
�������
�������$��1q� �� Padding$ 5q�._���  ��Name$ PART_ContentHost��.���� -   $��BTSearch��$��Search��$
��Left=�$��Center=�$��	636,0,0,0q�$��30��$��88��$��1���� -   $��
CBDropDown��$
��Left=�$��50,0,0,0q�$��Center=�$	��130��$��30�� �� SelectedIndex$ 0�����	#FFFF6CF3$��1������  �� EndPoint$	 0,1&�.���  � Color$ 	#FFF0F0F0��� $	 Red�� � Offset$ 1Q��� $��Center=�$��
0,21,0,-60q����#dbdbdb���#03346E$	��762��$��1��$��1���� -   $��BTUpload��$��Upload��$��80��$��30��$��1��$��668,415,52,-45q�$��1���� -   $��BTClear��$��Clear��$��80��$��30��$��1��$��560,415,160,-45q�$��1���� -   $��BTClose��$��Close��$��80��$��30��$��2��$��10,27,10,343q�$��1���� -   $��ListBox�����#E2E2B6
���   �
���   �$��2q�$��5q�$
��Auto=�$��1��$��52,47,52,179q�$��1������ " ��.���  � ��$ 5q���  � ��$ 10q���  � �� �#03346E��  � ��
 �������  � ��
 �������  � ��$ 1q���  � ��$ 14(���  � ��$
 Bold"���  � ��$ Stretch=���  � ��$ Center=��� -   $��ListBox2�����#E2E2B6
���   �
���   �$��2q�$��5q�$
��Auto=�$��1��$��50,226,52,0q�$��1������ " ��.���  � ��$ 5q���  � ��$ 10q���  � �� �#03346E��  � ��
 �������  � ��
 �������  � ��$ 1q���  � ��$ 14(���  � ��$
 Bold"���  � ��$ Stretch=���  � ��$ Center=� �� Triggers P�  P� Property �@J�IsMouseOver P� Value$
 True��. ���  � �� �#6EACDA��  � ��
 �   �P�  �@��
IsSelected$
 True��. ���  � �� �	#FF0882DC��  � ��
 �   �