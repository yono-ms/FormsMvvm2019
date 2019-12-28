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

# �J�X�^���R���g���[��

## �ړI

���i���J�X�^�}�C�Y����ۂɕ�����g�p����Ȃ�A
�ȉ��̍�Ƃ��s���B

- style�����
- �J�X�^���R���g���[�������

## ���_

XamarinForms�ɂ̓J�X�^���R���g���[���̎������@�����낢�날��A
�ǂ̕��@���̗p����ׂ��Ȃ̂��Y�ށB

## 2019�N�̃J�X�^���R���g���[������

�ȉ��ɊȒP�ȏ��ɐ�������B

### �h���N���X�����

Forms�݂̒邵���i�ɓ���̐ݒ���s���B
���Ԃ�style�Ŏ����ł��邱�Ƃ��\�[�X�ŏ����������ɂȂ�B
style�ŏ�����������R�X�g�Ȃ̂����������ׂ��B

### ContentView

Forms�݂̒邵�̕��i��g�ݍ��킹�Ĉ�̕��i�ɂ���B
�ˑ��֌W�v���p�e�B�����̂��ʓ|���������b�g�͑����B

- �v���b�g�t�H�[���Ɉˑ����Ȃ��R�[�h
- �f�U�C�i�[�v���r���[

### Behavior

Forms�݂̒邵�̕��i�ɓY�t�r�w�C�r�A����������B
�C�x���g��V����Command�ɕϊ��ł���B
�v���p�e�B���o�C���h���āA
VM����changed�n���h������邱�Ƃ��ł��邪�A
VM���`�����ɂ��ď�����ǂ��o�������ꍇ�Ɏg����B
�v���p�e�B�̃o�C���h�����ōςނ̂��ǂ�������������B

### Effect

Forms�݂̒邵�̕��i�Ƀv���b�g�t�H�[���ɑ��݂���v���p�e�B�������P�[�X�͑����B
Entry�̘g�����Ȃǂ��Y������B
Forms��Renderer�����Ŏ�������Ă���̂��𒲂ׂāA
�v���b�g�t�H�[���̃R�[�h�Œ����ł���ꍇ�Ɏg���B

### Renderer

�v���b�g�t�H�[���̕��i���g�������Ȃ̂ɁA
Forms�ɕ��i���p�ӂ���Ă��Ȃ��ꍇ�Ɏg�p����B
Forms�ɂ���ꍇ��Effect�A�����ꍇ��Renderer���̗p����B
����͂ق�XamarinNative�Ƃ������@�Ȃ̂ŁA
�\�[�X�͑S���ʕ��Ńv���b�g�t�H�[����ޕ������|���Z�ő����邱�ƂɂȂ�B

### SkiaSharp

�v���b�g�t�H�[���ɂ������B
����Ȃ�Forms����SkiaSharp���g�p����̂��R�X�g�I�ɂ����炩�B

### ���܂��FListView�͎g���Ȃ�

ListView���J�X�^�}�C�Y���ĉ��Ƃ����悤�Ǝv�����炢�Ȃ�A
����������܂��������Ƃ����P�[�X�͑����B
�݂邵��Cell���g���Ȃ��Ȃ����������߂悤�B
Cell�̃Z���N�^�[�Ƃ��͖ʓ|�ɂȂ邾���B
