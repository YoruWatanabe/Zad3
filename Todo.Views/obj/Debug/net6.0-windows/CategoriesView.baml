   M S B A M L   `   `   `  ����  
Todo.Views    Todo.Views.CategoriesView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        � 	Resources  5       �� MergedDictionaries �� Source�� 5   
    5	      �� 5
      $0 */Todo.Views;component/SharedResources.xaml?�6%   %    5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   ]� $��0��6   $��False��6!   .+�5      �� $��Add Category��6   $��Right=�6D   $��5q�6[      PrimaryButton#��[  6f   
 � Tag$	 ��6�   ��6,   �� *Add+6�   5      �� 5   
   $��1��6   $��0q�6C     ListViewPrimaryItem#��[ 6W   ��6    �� *
Categories+ �� View 5      �� 5      .P�5      �� $��Id��6%    �� DisplayMemberBinding 61   �� *
CategoryId+ �� CellTemplate 5      �� 5      .R�5   "   �� ��6,   �� *
CategoryId+5      5      �� 6'   $
��Name��66    6D   �� *Name+ 5       �� 5!      .R�5"   "   �� ��6,   �� *Name+5#      5%      �� 5&      $��Tasks��6%    �� Width$	 150��64    5'      �� 5(      .R�5)   "   9� $
��View��6+   $��2q�6:   .��5*   &   �� ��63   �� *Tasks+��5+   *   �� 5,   .   .R�5-   2   � $��0,0,0,5q�67   .+�5.   6   �� ��6@   �� *Name+5/   3   50   /   52   '   53   #   54      56      �� 57      $
��Edit��6%    58      �� 59      .R�5:   "   ��   SecondaryButton#��[ 6�   $	 ��6�   ��6)   �� *DataContext.Edit+	 �� RelativeSource	 �� *FindAncestor+
 �� AncestorType
 UserControl��6�   �� *
CategoryId+6�   5;      5=      �� 5>      $��Remove��6%    5?      �� 5@      .R�5A   "   �� #��[ 6�   $	 ��6�   ��6)   �� *DataContext.Remove+	 �� *FindAncestor+
 UserControl��6�   �� *
CategoryId+6�   5B      5D      5E      5G      5H      5I      