   M S B A M L   `   `   `  ����  在线答疑系统.    (QuestionAnsweringSystem.MyQuestionAnswer   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$  MyQuestionAnswer��5   	    � Icon$ icon.jpg��6"   $	��500��62   $	��700��6?   .��5      � 
�������.+�5	   
   �� -   5        $��LbMyQuestionAnswer��5	      $��1��6,   $	��700��69   $	��300��6E   
���   � "  MaterialDesignCardsListBoxItem#��[  5
      ��5      �� 5      .R�5      �  � ColumnDefinitions 5      �� 5      $��80 �6/   �� 5      $��80 �6/   �� 5      $��80 �6/   �� 5       � RowDefinitions 5      �� 5      $��30 �6,   �� 5      $	��160 �6,   .+�5      �� $��
LbHeadshot��5   K   $��30��5   D   $��Center=�6P   $��Center=�5   0   $
��Fill=�6]   $��30��6l   ��5       ��  �� Path$ 	headimage� J� Clip 5      @� 5   "    @� RadiusX$ 15Q�62    @� RadiusY$ 15Q�6?    @� Center$ 15,15&�6L   5      �� 5      $��1��6$   
��� � �$��20(�6b   $��80��6p    " MaterialDesignCaptionTextBlock#��[ 5   0   ��5   4   �� $
 role�5   0   �� 5       $��2��6$   
���   �$��20(�6b   $��60��6p   #��[ 5!   4   ��5    4   �� $
 name�5!   4   �� 5"      $��1��6$   $��4��61   $
��Left=�5#   2   $	��650��6M   $
��Wrap=�6\   $	��Top=�6p   #��[ 5$   2   $��40��6j   $��20(�6y   	 �� Padding$	 20q�6�   ��5"   E   �� $ acontent�5$   �   5%      5&      5(      5)      5*      