   M S B A M L   `   `   `  ����  
Todo.Views    Todo.Views.EditTaskView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        � 	Resources  5       �� MergedDictionaries �� Source�� 5   
    5	      �� 5
      $0 */Todo.Views;component/SharedResources.xaml?�6%   %    5      .��5      � $	��293��6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $
��Back��6   $��0��6    $��0��6-   $��5,5,5,0q�6V   $	��100��6g   $
��Left=�6s      PrimaryButton#��[  6�   
 � Tag$	 ��6�   ��6=   �� *Back+6�   �� 5   
   $
��Save��6   $��0��6    $��1��6-   $��5,5,5,0q�6V   $	��100��6g   $��Right=�6s   #��[  6�   $	 ��6�   ��6=   �� *Save+6�   �� 5   
   $��Personal Data��6   $��1��6*   $��0��67   $��5,5,5,5q�6G   $��10q�6X   $��2��6e   .��5      �� .+�5      � $��0,0,0,5q�6   .+�5      �� $��Task ID��6      PrimaryTextBlock#��[ 6/   �� 5        PrimaryTextBox#��[ 6R   ��6   �� *TaskId+ �� ValidatesOnDataErrors$
 True��6R   5      � 5       $��0,0,0,5q�6   .+�5!      �� $
��Name��6    #��[ 6,   �� 5"      #��[ 6P   ��6   �� *Name+$
 True��6P   5#      � 5$      $��0,0,0,5q�6   .+�5%      �� $��Description��6    #��[ 63   �� 5&      #��[ 6W   ��6   �� *Description+$
 True��6W   5'      � 5(      $��0,0,0,5q�6   .+�5)      �� $��Category Id��6    #��[ 63   �� 5*      #��[ 6V   ��6   �� *
CategoryId+$
 True��6V   5+      � 5,      $��0,0,0,5q�6   .+�5-      �� $��User Id��6    #��[ 6/   �� 5.      #��[ 6R   ��6   �� *UserId+$
 True��6R   5/      � 50      $��0,0,0,5q�6   .+�51      �� $��
Start Date��6    #��[ 62   :  4System.Windows.Controls.Primitives.DatePickerTextBox  52      #��[ 6_   ��6(   �� *	StartDate+$
 True��6_   53      � 54      $��0,0,0,5q�6   .+�55      �� $��Due Date��6    #��[ 60     56      #��[ 6]   ��6(   �� *	DueDate+$
 True��6]   57      � 58      $��0,0,0,5q�6   .+�59      �� $��
Importance��6    #��[ 62   �� 5:      -   5        $��comboBoxImportance��5:   W    �� Text 6   �� *
Importance+$
 True��6W   5;      � 5<      $��0,0,0,5q�6   .+�5=      �� $��	Completed��6    #��[ 61   �� 5>       d� 	IsChecked 6   �� *IsCompleted+$
 True��5?      5@      5A      �� 5B   
   $��2��6.   $��2��6;   $��Center=�6O   $��0,0,0,5q�6l   ��6   �� *
Response+6l   5C      5D      