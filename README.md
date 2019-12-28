# FormsMvvm2019
Xamarin Forms Mvvm ver 2019.

# ViewModel����

## �ړI

ViewModel��Page�Ƃ͕ʃN���X�ɂ������v�]�͑����B

## �]���̎��s��

### �R�[�h�r�n�C���h��Binding

�R�[�h�r�n�C���h�Ńo�C���h����Ǝ��s���Ɋ��ғ���ƂȂ邪�A
xaml�G�f�B�^�[�ł̃C���e���Z���X�������Ȃ��B

������������邽�߂�Page��StaticResource������āA
d:BindingContext�ɃZ�b�g������@���l�����B
�������A���ꂾ�ƃC���X�^���X��2�ł��Ă��܂��B

ViewModel�R���X�g���N�^�ɏ����������Ă͂����Ȃ����A
���������Ă��������K���s����Ă��܂��B
�������g�p�ʂ̓v���p�e�B�����Ȃ̂ŁA
�C�ɂȂ�قǑ傫���͂Ȃ�Ȃ��͂�������{�ɂȂ��Ă���B

### ViewModel��Command

�o�C���h�Ɉˑ����ăC�x���g�n���h����VM�ɏ����Ă���B
��ʈˑ����Ȃ�����������̂łł���Ȃ番���������B

## 2019�N�̔���

### Page��BindingContext���g�����@

```
<ContentPage.BindingContext>
    <local:MainViewModel/>
</ContentPage.BindingContext>
```

����ŃC���X�^�������ƃo�C���h�������ɂł���B
���R�C���e���Z���X�������B
�C���X�^���X��Page�R���X�g���N�^�ō쐬���ꂽ1�����B

### Command��static�N���X�ɏ������@

```
<Button
    Text="{Binding ButtonCommitText}"
    Command="{x:Static local:BizLogic.Command}"
    CommandParameter="{Binding}"/>
```

x:Static��static�N���X�̃v���p�e�B���o�C���h�ł���B
������Binding���������Ă�����BindingContext(=ViewModel)��
�n�����B

```
static class BizLogic
{
    static public Command Command => new Command((args) =>
    {
        switch (args)
        {
            case MainViewModel mainViewModel:
                Application.Current.MainPage.DisplayAlert("BizLogic", $"PassCode={mainViewModel.PassCode}", "OK");
                break;

            default:
                Application.Current.MainPage.DisplayAlert("BizLogic", $"unknown args={args}", "OK");
                break;
        }
    });
}
```

Command�̃v���p�e�B���𕪂��邩�A
�������肷�邩�̓P�[�X�o�C�P�[�X�B
�^switch���g����̂ŒZ�������Ȃ炱�ꂪ�ȒP�ɏ�����B

# ���[�J�����O

## �ړI

�f�o�C�X���Ƀ��O�t�@�C�����c�������Ƃ����v�]�͑����B
NLog�����I�������������AILogger�Ŏg�������B
���O�����������ւ������Ƃ��ɑS�t�@�C����NLog�^�����݂���͍̂���B

## ���_

AddProvider��NLog���g����Nuget�����邪�A
Release�r���h�Ń����J�[�����\�b�h�������Ă��܂��s�������B

## 2019�N�̔���

### AppLogger�N���X�����

ILogger�̎����������GetLogger��񋟂���΂����B
���̒���NLog�ɕϊ����Ďg���Ă����ƁA
NLog�p�~�ƂȂ��Ă��_���[�W���قƂ�ǂȂ��B

NLog��Console��UWP�����f�o�b�K�ɏo�Ȃ������Ή��ł���B
