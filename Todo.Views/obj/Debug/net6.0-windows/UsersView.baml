   M S B A M L   `   `   `  ����  
Todo.Views    Todo.Views.UsersView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        � 	Resources  5       �� MergedDictionaries �� Source�� 5   
    5	      �� 5
      $0 */Todo.Views;component/SharedResources.xaml?�6%   %    5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   ]� $��0��6   $��False��6!   .+�5      �� $��Add User��6   $��Right=�6@   $��5q�6W      PrimaryButton#��[  6b   
 � Tag$	 ��6�   ��6(   �� *Add+6�   5      �� 5   
   $��1��6   $��0q�6>     ListViewPrimaryItem#��[ 6R   ��6    �� *Users+ �� View 5      �� 5      .P�5      �� $��Id��6%    �� DisplayMemberBinding 61   �� *UserId+ �� CellTemplate 5      �� 5      .R�5   "   �� ��6,   �� *UserId+5      5      �� 5       $��
First Name��6%    69   �� *	FirstName+ 5!      �� 5"      .R�5#   "   �� ��6,   �� *	FirstName+5$      5&      �� 5'      $��	Last Name��6%    68   �� *
LastName+ 5(      �� 5)      .R�5*   "   �� ��6,   �� *
LastName+5+      5-      �� 5.      $��Tasks��6%    �� Width$	 150��64    5/      �� 50      .R�51   "   9� $
��View��6+   $��2q�6:   .��52   &   �� ��63   �� *Tasks+��53   *   �� 54   .   .R�55   2   � $��0,0,0,5q�67   .+�56   6   �� ��6@   �� *Name+57   3   58   /   5:   '   5;   #   5<      5>      �� 5?      $
��Edit��6%    5@      �� 5A      .R�5B   "   ��   SecondaryButton#��[ 6�   $	 ��6�   ��6)   �� *DataContext.Edit+	 �� RelativeSource	 �� *FindAncestor+
 �� AncestorType
 UserControl��6�   �� *UserId+6�   5C      5E      �� 5F      $��Remove��6%    5G      �� 5H      .R�5I   "   �� #��[ 6�   $	 ��6�   ��6)   �� *DataContext.Remove+	 �� *FindAncestor+
 UserControl��6�   �� *UserId+6�   5J      5L      5M      5O      5P      5Q      