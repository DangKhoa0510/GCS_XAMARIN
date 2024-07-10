; ModuleID = 'obj\Debug\90\android\marshal_methods.x86_64.ll'
source_filename = "obj\Debug\90\android\marshal_methods.x86_64.ll"
target datalayout = "e-m:e-p270:32:32-p271:32:32-p272:64:64-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-unknown-linux-android"


%struct.MonoImage = type opaque

%struct.MonoClass = type opaque

%struct.MarshalMethodsManagedClass = type {
	i32,; uint32_t token
	%struct.MonoClass*; MonoClass* klass
}

%struct.MarshalMethodName = type {
	i64,; uint64_t id
	i8*; char* name
}

%class._JNIEnv = type opaque

%class._jobject = type {
	i8; uint8_t b
}

%class._jclass = type {
	i8; uint8_t b
}

%class._jstring = type {
	i8; uint8_t b
}

%class._jthrowable = type {
	i8; uint8_t b
}

%class._jarray = type {
	i8; uint8_t b
}

%class._jobjectArray = type {
	i8; uint8_t b
}

%class._jbooleanArray = type {
	i8; uint8_t b
}

%class._jbyteArray = type {
	i8; uint8_t b
}

%class._jcharArray = type {
	i8; uint8_t b
}

%class._jshortArray = type {
	i8; uint8_t b
}

%class._jintArray = type {
	i8; uint8_t b
}

%class._jlongArray = type {
	i8; uint8_t b
}

%class._jfloatArray = type {
	i8; uint8_t b
}

%class._jdoubleArray = type {
	i8; uint8_t b
}

; assembly_image_cache
@assembly_image_cache = local_unnamed_addr global [0 x %struct.MonoImage*] zeroinitializer, align 8
; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = local_unnamed_addr constant [304 x i64] [
	i64 15690660930947125, ; 0: Microsoft.DotNet.PlatformAbstractions.dll => 0x37be92af148835 => 26
	i64 45886493525149769, ; 1: Xamarin.Forms.Material => 0xa30585d28e0849 => 117
	i64 98382396393917666, ; 2: Microsoft.Extensions.Primitives.dll => 0x15d8644ad360ce2 => 42
	i64 120698629574877762, ; 3: Mono.Android => 0x1accec39cafe242 => 43
	i64 195258733703605363, ; 4: System.Net.Http.Formatting => 0x2b5b2c8a5b22c73 => 68
	i64 196720943101637631, ; 5: System.Linq.Expressions.dll => 0x2bae4a7cd73f3ff => 150
	i64 233781196042969304, ; 6: NGraphics.Android => 0x33e8ec2594ea0d8 => 50
	i64 263803244706540312, ; 7: Rg.Plugins.Popup.dll => 0x3a937a743542b18 => 52
	i64 590536689428908136, ; 8: Xamarin.Android.Arch.Lifecycle.ViewModel.dll => 0x83201fd803ec868 => 84
	i64 668723562677762733, ; 9: Microsoft.Extensions.Configuration.Binder.dll => 0x947c88986577aad => 34
	i64 702024105029695270, ; 10: System.Drawing.Common => 0x9be17343c0e7726 => 131
	i64 799765834175365804, ; 11: System.ComponentModel.dll => 0xb1956c9f18442ac => 15
	i64 816102801403336439, ; 12: Xamarin.Android.Support.Collections => 0xb53612c89d8d6f7 => 88
	i64 846634227898301275, ; 13: Xamarin.Android.Arch.Lifecycle.LiveData.Core => 0xbbfd9583890bb5b => 81
	i64 870603111519317375, ; 14: SQLitePCLRaw.lib.e_sqlite3.android => 0xc1500ead2756d7f => 56
	i64 923776495456941846, ; 15: SuaveControls.DynamicStackLayout.dll => 0xcd1e9d40790f716 => 58
	i64 940822596282819491, ; 16: System.Transactions => 0xd0e792aa81923a3 => 129
	i64 985332033409892329, ; 17: System.Net.Http.Primitives => 0xdac9a438d35bfe9 => 69
	i64 996343623809489702, ; 18: Xamarin.Forms.Platform => 0xdd3b93f3b63db26 => 119
	i64 1000557547492888992, ; 19: Mono.Security.dll => 0xde2b1c9cba651a0 => 147
	i64 1010800728818218806, ; 20: Microsoft.Bcl.HashCode.dll => 0xe0715e84bea7736 => 23
	i64 1301485588176585670, ; 21: SQLitePCLRaw.core => 0x120fce3f338e43c6 => 55
	i64 1339544861943895157, ; 22: NGraphics.dll => 0x129704f468dd9475 => 51
	i64 1342439039765371018, ; 23: Xamarin.Android.Support.Fragment => 0x12a14d31b1d4d88a => 99
	i64 1425944114962822056, ; 24: System.Runtime.Serialization.dll => 0x13c9f89e19eaf3a8 => 14
	i64 1476839205573959279, ; 25: System.Net.Primitives.dll => 0x147ec96ece9b1e6f => 148
	i64 1480970664966385785, ; 26: XLabs.Core.dll => 0x148d76f928187079 => 121
	i64 1490981186906623721, ; 27: Xamarin.Android.Support.VersionedParcelable => 0x14b1077d6c5c0ee9 => 113
	i64 1518315023656898250, ; 28: SQLitePCLRaw.provider.e_sqlite3 => 0x151223783a354eca => 57
	i64 1672383392659050004, ; 29: Microsoft.Data.Sqlite.dll => 0x17357fd5bfb48e14 => 25
	i64 1731380447121279447, ; 30: Newtonsoft.Json => 0x18071957e9b889d7 => 49
	i64 1743969030606105336, ; 31: System.Memory.dll => 0x1833d297e88f2af8 => 65
	i64 1744702963312407042, ; 32: Xamarin.Android.Support.v7.AppCompat => 0x18366e19eeceb202 => 109
	i64 1817258447273074901, ; 33: NControl.Droid.dll => 0x193832edf6ca20d5 => 47
	i64 1860886102525309849, ; 34: Xamarin.Android.Support.v7.RecyclerView.dll => 0x19d3320d047b7399 => 111
	i64 1865037103900624886, ; 35: Microsoft.Bcl.AsyncInterfaces => 0x19e1f15d56eb87f6 => 22
	i64 1938067011858688285, ; 36: Xamarin.Android.Support.v4.dll => 0x1ae565add0bd691d => 108
	i64 2040001226662520565, ; 37: System.Threading.Tasks.Extensions.dll => 0x1c4f8a4ea894a6f5 => 2
	i64 2133195048986300728, ; 38: Newtonsoft.Json.dll => 0x1d9aa1984b735138 => 49
	i64 2192948757939169934, ; 39: Microsoft.EntityFrameworkCore.Abstractions.dll => 0x1e6eeb46cf992a8e => 27
	i64 2284400282711631002, ; 40: System.Web.Services => 0x1fb3d1f42fd4249a => 134
	i64 2287834202362508563, ; 41: System.Collections.Concurrent => 0x1fc00515e8ce7513 => 13
	i64 2287887973817120656, ; 42: System.ComponentModel.DataAnnotations.dll => 0x1fc035fd8d41f790 => 135
	i64 2335503487726329082, ; 43: System.Text.Encodings.Web => 0x2069600c4d9d1cfa => 74
	i64 2337758774805907496, ; 44: System.Runtime.CompilerServices.Unsafe => 0x207163383edbc828 => 72
	i64 2497223385847772520, ; 45: System.Runtime => 0x22a7eb7046413568 => 73
	i64 2592350477072141967, ; 46: System.Xml.dll => 0x23f9e10627330e8f => 76
	i64 2624866290265602282, ; 47: mscorlib.dll => 0x246d65fbde2db8ea => 45
	i64 2656907746661064104, ; 48: Microsoft.Extensions.DependencyInjection => 0x24df3b84c8b75da8 => 37
	i64 2783046991838674048, ; 49: System.Runtime.CompilerServices.Unsafe.dll => 0x269f5e7e6dc37c80 => 72
	i64 2949706848458024531, ; 50: Xamarin.Android.Support.SlidingPaneLayout => 0x28ef76c01de0a653 => 105
	i64 2960931600190307745, ; 51: Xamarin.Forms.Core => 0x2917579a49927da1 => 116
	i64 2977248461349026546, ; 52: Xamarin.Android.Support.DrawerLayout => 0x29514fb392c97af2 => 98
	i64 3022227708164871115, ; 53: Xamarin.Android.Support.Media.Compat.dll => 0x29f11c168f8293cb => 103
	i64 3106852385031680087, ; 54: System.Runtime.Serialization.Xml => 0x2b1dc1c88b637057 => 151
	i64 3184279552076006120, ; 55: XLabs.Core => 0x2c30d561af07c2e8 => 121
	i64 3311221304742556517, ; 56: System.Numerics.Vectors.dll => 0x2df3d23ba9e2b365 => 71
	i64 3325875462027654285, ; 57: System.Runtime.Numerics => 0x2e27e21c8958b48d => 9
	i64 3421039665716491404, ; 58: XLabs.Serialization.dll => 0x2f79f973558d748c => 127
	i64 3494946837667399002, ; 59: Microsoft.Extensions.Configuration => 0x30808ba1c00a455a => 35
	i64 3523004241079211829, ; 60: Microsoft.Extensions.Caching.Memory.dll => 0x30e439b10bb89735 => 32
	i64 3531994851595924923, ; 61: System.Numerics => 0x31042a9aade235bb => 70
	i64 3571415421602489686, ; 62: System.Runtime.dll => 0x319037675df7e556 => 73
	i64 3638003163729360188, ; 63: Microsoft.Extensions.Configuration.Abstractions => 0x327cc89a39d5f53c => 33
	i64 3647754201059316852, ; 64: System.Xml.ReaderWriter => 0x329f6d1e86145474 => 143
	i64 3655542548057982301, ; 65: Microsoft.Extensions.Configuration.dll => 0x32bb18945e52855d => 35
	i64 3716579019761409177, ; 66: netstandard.dll => 0x3393f0ed5c8c5c99 => 1
	i64 3869221888984012293, ; 67: Microsoft.Extensions.Logging.dll => 0x35b23cceda0ed605 => 40
	i64 3953705865940352863, ; 68: XLabs.Platform.dll => 0x36de628995c0bf5f => 125
	i64 3966267475168208030, ; 69: System.Memory => 0x370b03412596249e => 65
	i64 4009997192427317104, ; 70: System.Runtime.Serialization.Primitives => 0x37a65f335cf1a770 => 141
	i64 4154383907710350974, ; 71: System.ComponentModel => 0x39a7562737acb67e => 15
	i64 4187479170553454871, ; 72: System.Linq.Expressions => 0x3a1cea1e912fa117 => 150
	i64 4202567570116092282, ; 73: Newtonsoft.Json.Bson => 0x3a5284f05958a17a => 48
	i64 4252163538099460320, ; 74: Xamarin.Android.Support.ViewPager.dll => 0x3b02b8357f4958e0 => 114
	i64 4264996749430196783, ; 75: Xamarin.Android.Support.Transition.dll => 0x3b304ff259fb8a2f => 107
	i64 4337444564132831293, ; 76: SQLitePCLRaw.batteries_v2.dll => 0x3c31b2d9ae16203d => 54
	i64 4349341163892612332, ; 77: Xamarin.Android.Support.DocumentFile => 0x3c5bf6bea8cd9cec => 97
	i64 4416987920449902723, ; 78: Xamarin.Android.Support.AsyncLayoutInflater.dll => 0x3d4c4b1c879b9883 => 87
	i64 4513320955448359355, ; 79: Microsoft.EntityFrameworkCore.Relational => 0x3ea2897f12d379bb => 29
	i64 4525561845656915374, ; 80: System.ServiceModel.Internals => 0x3ece06856b710dae => 136
	i64 4612482779465751747, ; 81: Microsoft.EntityFrameworkCore.Abstractions => 0x4002d4a662a99cc3 => 27
	i64 4641342514663044372, ; 82: XLabs.Ioc.dll => 0x40695c6d1b68b514 => 124
	i64 4743821336939966868, ; 83: System.ComponentModel.Annotations => 0x41d5705f4239b194 => 3
	i64 4841234827713643511, ; 84: Xamarin.Android.Support.CoordinaterLayout => 0x432f856d041f03f7 => 90
	i64 4963205065368577818, ; 85: Xamarin.Android.Support.LocalBroadcastManager.dll => 0x44e0d8b5f4b6a71a => 102
	i64 5081566143765835342, ; 86: System.Resources.ResourceManager.dll => 0x4685597c05d06e4e => 12
	i64 5099468265966638712, ; 87: System.Resources.ResourceManager => 0x46c4f35ea8519678 => 12
	i64 5129462924058778861, ; 88: Microsoft.Data.Sqlite => 0x472f835a350f5ced => 25
	i64 5142919913060024034, ; 89: Xamarin.Forms.Platform.Android.dll => 0x475f52699e39bee2 => 118
	i64 5178572682164047940, ; 90: Xamarin.Android.Support.Print.dll => 0x47ddfc6acbee1044 => 104
	i64 5203618020066742981, ; 91: Xamarin.Essentials => 0x4836f704f0e652c5 => 115
	i64 5288341611614403055, ; 92: Xamarin.Android.Support.Interpolator.dll => 0x4963f6ad4b3179ef => 100
	i64 5439315836349573567, ; 93: Xamarin.Android.Support.Animated.Vector.Drawable.dll => 0x4b7c54ef36c5e9bf => 85
	i64 5446034149219586269, ; 94: System.Diagnostics.Debug => 0x4b94333452e150dd => 6
	i64 5507995362134886206, ; 95: System.Core.dll => 0x4c705499688c873e => 61
	i64 5767696078500135884, ; 96: Xamarin.Android.Support.Annotations.dll => 0x500af9065b6a03cc => 86
	i64 6044705416426755068, ; 97: Xamarin.Android.Support.SwipeRefreshLayout.dll => 0x53e31b8ccdff13fc => 106
	i64 6085203216496545422, ; 98: Xamarin.Forms.Platform.dll => 0x5472fc15a9574e8e => 119
	i64 6086316965293125504, ; 99: FormsViewGroup.dll => 0x5476f10882baef80 => 19
	i64 6183170893902868313, ; 100: SQLitePCLRaw.batteries_v2 => 0x55cf092b0c9d6f59 => 54
	i64 6222399776351216807, ; 101: System.Text.Json.dll => 0x565a67a0ffe264a7 => 75
	i64 6311200438583329442, ; 102: Xamarin.Android.Support.LocalBroadcastManager => 0x5795e35c580c82a2 => 102
	i64 6405879832841858445, ; 103: Xamarin.Android.Support.Vector.Drawable.dll => 0x58e641c4a660ad8d => 112
	i64 6489849135769361525, ; 104: XLabs.Forms.Droid.dll => 0x5a1093677f689c75 => 123
	i64 6560151584539558821, ; 105: Microsoft.Extensions.Options => 0x5b0a571be53243a5 => 41
	i64 6588599331800941662, ; 106: Xamarin.Android.Support.v4 => 0x5b6f682f335f045e => 108
	i64 6591024623626361694, ; 107: System.Web.Services.dll => 0x5b7805f9751a1b5e => 134
	i64 6814185388980153342, ; 108: System.Xml.XDocument.dll => 0x5e90d98217d1abfe => 142
	i64 6876862101832370452, ; 109: System.Xml.Linq => 0x5f6f85a57d108914 => 77
	i64 6894844156784520562, ; 110: System.Numerics.Vectors => 0x5faf683aead1ad72 => 71
	i64 7194160955514091247, ; 111: Xamarin.Android.Support.CursorAdapter.dll => 0x63d6cb45d266f6ef => 93
	i64 7270811800166795866, ; 112: System.Linq => 0x64e71ccf51a90a5a => 140
	i64 7338192458477945005, ; 113: System.Reflection => 0x65d67f295d0740ad => 146
	i64 7473077275758116397, ; 114: Microsoft.DotNet.PlatformAbstractions => 0x67b5b430309b3e2d => 26
	i64 7488575175965059935, ; 115: System.Xml.Linq.dll => 0x67ecc3724534ab5f => 77
	i64 7489048572193775167, ; 116: System.ObjectModel => 0x67ee71ff6b419e3f => 149
	i64 7618414623594068305, ; 117: NControl.Droid => 0x69ba0bbc6ef73951 => 47
	i64 7635363394907363464, ; 118: Xamarin.Forms.Core.dll => 0x69f6428dc4795888 => 116
	i64 7637365915383206639, ; 119: Xamarin.Essentials.dll => 0x69fd5fd5e61792ef => 115
	i64 7654504624184590948, ; 120: System.Net.Http => 0x6a3a4366801b8264 => 66
	i64 7735176074855944702, ; 121: Microsoft.CSharp => 0x6b58dda848e391fe => 24
	i64 7820441508502274321, ; 122: System.Data => 0x6c87ca1e14ff8111 => 128
	i64 7821246742157274664, ; 123: Xamarin.Android.Support.AsyncLayoutInflater => 0x6c8aa67926f72e28 => 87
	i64 7875371864198757046, ; 124: AndHUD.dll => 0x6d4af0fc27ac3ab6 => 17
	i64 7879037620440914030, ; 125: Xamarin.Android.Support.v7.AppCompat.dll => 0x6d57f6f88a51d86e => 109
	i64 7972383140441761405, ; 126: Microsoft.Extensions.Caching.Abstractions.dll => 0x6ea3983a0b58267d => 31
	i64 8044118961405839122, ; 127: System.ComponentModel.Composition => 0x6fa2739369944712 => 133
	i64 8064050204834738623, ; 128: System.Collections.dll => 0x6fe942efa61731bf => 8
	i64 8087206902342787202, ; 129: System.Diagnostics.DiagnosticSource => 0x703b87d46f3aa082 => 62
	i64 8101777744205214367, ; 130: Xamarin.Android.Support.Annotations => 0x706f4beeec84729f => 86
	i64 8103644804370223335, ; 131: System.Data.DataSetExtensions.dll => 0x7075ee03be6d50e7 => 130
	i64 8113615946733131500, ; 132: System.Reflection.Extensions => 0x70995ab73cf916ec => 16
	i64 8167236081217502503, ; 133: Java.Interop.dll => 0x7157d9f1a9b8fd27 => 21
	i64 8185542183669246576, ; 134: System.Collections => 0x7198e33f4794aa70 => 8
	i64 8196541262927413903, ; 135: Xamarin.Android.Support.Interpolator => 0x71bff6d9fb9ec28f => 100
	i64 8290740647658429042, ; 136: System.Runtime.Extensions => 0x730ea0b15c929a72 => 139
	i64 8318905602908530212, ; 137: System.ComponentModel.DataAnnotations => 0x7372b092055ea624 => 135
	i64 8385935383968044654, ; 138: Xamarin.Android.Arch.Lifecycle.Runtime.dll => 0x7460d3cd16cb566e => 83
	i64 8518412311883997971, ; 139: System.Collections.Immutable => 0x76377add7c28e313 => 60
	i64 8626175481042262068, ; 140: Java.Interop => 0x77b654e585b55834 => 21
	i64 8638972117149407195, ; 141: Microsoft.CSharp.dll => 0x77e3cb5e8b31d7db => 24
	i64 8650936586195284951, ; 142: ExifLib => 0x780e4cfd9299b3d7 => 18
	i64 8684531736582871431, ; 143: System.IO.Compression.FileSystem => 0x7885a79a0fa0d987 => 132
	i64 8725526185868997716, ; 144: System.Diagnostics.DiagnosticSource.dll => 0x79174bd613173454 => 62
	i64 8747388101076821409, ; 145: XLabs.Forms.dll => 0x7964f721dd29a5a1 => 122
	i64 8796457469147618732, ; 146: Xamarin.Android.Support.CustomTabs => 0x7a134b766a601dac => 94
	i64 8808820144457481518, ; 147: Xamarin.Android.Support.Loader.dll => 0x7a3f374010b17d2e => 101
	i64 8917102979740339192, ; 148: Xamarin.Android.Support.DocumentFile.dll => 0x7bbfe9ea4d000bf8 => 97
	i64 8941376889969657626, ; 149: System.Xml.XDocument => 0x7c1626e87187471a => 142
	i64 9094004549068921173, ; 150: System.Net.Http.Extensions => 0x7e3464f48d0a1955 => 67
	i64 9111603110219107042, ; 151: Microsoft.Extensions.Caching.Memory => 0x7e72eac0def44ae2 => 32
	i64 9250544137016314866, ; 152: Microsoft.EntityFrameworkCore => 0x806088e191ee0bf2 => 28
	i64 9290408134796603763, ; 153: Xamarin.Forms.Material.dll => 0x80ee28f9d4f37173 => 117
	i64 9475595603812259686, ; 154: Xamarin.Android.Support.Design => 0x838013ff707b9766 => 96
	i64 9584643793929893533, ; 155: System.IO.dll => 0x85037ebfbbd7f69d => 137
	i64 9601112791485882581, ; 156: XLabs.Serialization => 0x853e013708f9a0d5 => 127
	i64 9659729154652888475, ; 157: System.Text.RegularExpressions => 0x860e407c9991dd9b => 138
	i64 9662334977499516867, ; 158: System.Numerics.dll => 0x8617827802b0cfc3 => 70
	i64 9709091224615084723, ; 159: XLabs.Platform.Droid => 0x86bd9f071f0b62b3 => 126
	i64 9779207461721759568, ; 160: XLabs.Ioc => 0x87b6b95fbcc65f50 => 124
	i64 9780723996889434231, ; 161: AndHUD => 0x87bc1ca798bbc877 => 17
	i64 9808709177481450983, ; 162: Mono.Android.dll => 0x881f890734e555e7 => 43
	i64 9834056768316610435, ; 163: System.Transactions.dll => 0x8879968718899783 => 129
	i64 9864956466380592553, ; 164: Microsoft.EntityFrameworkCore.Sqlite => 0x88e75da3af4ed5a9 => 30
	i64 9866412715007501892, ; 165: Xamarin.Android.Arch.Lifecycle.Common.dll => 0x88ec8a16fd6b6644 => 80
	i64 9983194123973340542, ; 166: XLabs.Platform.Droid.dll => 0x8a8b6e299b32ad7e => 126
	i64 9994308163963284983, ; 167: Newtonsoft.Json.Bson.dll => 0x8ab2ea52b0d16df7 => 48
	i64 9998632235833408227, ; 168: Mono.Security => 0x8ac2470b209ebae3 => 147
	i64 10038780035334861115, ; 169: System.Net.Http.dll => 0x8b50e941206af13b => 66
	i64 10205853378024263619, ; 170: Microsoft.Extensions.Configuration.Binder => 0x8da279930adb4fc3 => 34
	i64 10303855825347935641, ; 171: Xamarin.Android.Support.Loader => 0x8efea647eeb3fd99 => 101
	i64 10360651442923773544, ; 172: System.Text.Encoding => 0x8fc86d98211c1e68 => 7
	i64 10363495123250631811, ; 173: Xamarin.Android.Support.Collections.dll => 0x8fd287e80cd8d483 => 88
	i64 10447083246144586668, ; 174: Microsoft.Bcl.AsyncInterfaces.dll => 0x90fb7edc816203ac => 22
	i64 10566960649245365243, ; 175: System.Globalization.dll => 0x92a562b96dcd13fb => 10
	i64 10635644668885628703, ; 176: Xamarin.Android.Support.DrawerLayout.dll => 0x93996679ee34771f => 98
	i64 10714184849103829812, ; 177: System.Runtime.Extensions.dll => 0x94b06e5aa4b4bb34 => 139
	i64 10785150219063592792, ; 178: System.Net.Primitives => 0x95ac8cfb68830758 => 148
	i64 10811915265162633087, ; 179: Microsoft.EntityFrameworkCore.Relational.dll => 0x960ba3a651a45f7f => 29
	i64 10820705389670725792, ; 180: GCS_CPC_EMEC => 0x962ade38aa7a78a0 => 20
	i64 10850923258212604222, ; 181: Xamarin.Android.Arch.Lifecycle.Runtime => 0x9696393672c9593e => 83
	i64 10913891249535884439, ; 182: Xamarin.Android.Support.CustomTabs.dll => 0x9775ee4465d49c97 => 94
	i64 10964653383833615866, ; 183: System.Diagnostics.Tracing => 0x982a4628ccaffdfa => 145
	i64 11002576679268595294, ; 184: Microsoft.Extensions.Logging.Abstractions => 0x98b1013215cd365e => 39
	i64 11023048688141570732, ; 185: System.Core => 0x98f9bc61168392ac => 61
	i64 11037814507248023548, ; 186: System.Xml => 0x992e31d0412bf7fc => 76
	i64 11226290749488709958, ; 187: Microsoft.Extensions.Options.dll => 0x9bcbcbf50c874146 => 41
	i64 11376461258732682436, ; 188: Xamarin.Android.Support.Compat => 0x9de14f3d5fc13cc4 => 89
	i64 11395105072750394936, ; 189: Xamarin.Android.Support.v7.CardView => 0x9e238bb09789fe38 => 110
	i64 11398376662953476300, ; 190: Microsoft.EntityFrameworkCore.Sqlite.dll => 0x9e2f2b2f0b71c0cc => 30
	i64 11446671985764974897, ; 191: Mono.Android.Export => 0x9edabf8623efc131 => 44
	i64 11485890710487134646, ; 192: System.Runtime.InteropServices => 0x9f6614bf0f8b71b6 => 144
	i64 11530571088791430846, ; 193: Microsoft.Extensions.Logging => 0xa004d1504ccd66be => 40
	i64 11597940890313164233, ; 194: netstandard => 0xa0f429ca8d1805c9 => 1
	i64 11739066727115742305, ; 195: SQLite-net.dll => 0xa2e98afdf8575c61 => 53
	i64 11743665907891708234, ; 196: System.Threading.Tasks => 0xa2f9e1ec30c0214a => 5
	i64 11806260347154423189, ; 197: SQLite-net => 0xa3d8433bc5eb5d95 => 53
	i64 11815834011198458215, ; 198: NGraphics.Android.dll => 0xa3fa466e22768967 => 50
	i64 11834399401546345650, ; 199: Xamarin.Android.Support.SlidingPaneLayout.dll => 0xa43c3b8deb43ecb2 => 105
	i64 11865714326292153359, ; 200: Xamarin.Android.Arch.Lifecycle.LiveData => 0xa4ab7c5000e8440f => 82
	i64 11926601729614938946, ; 201: GCS_CPC_EMEC.dll => 0xa583cd154f420342 => 20
	i64 12102847907131387746, ; 202: System.Buffers => 0xa7f5f40c43256f62 => 59
	i64 12123043025855404482, ; 203: System.Reflection.Extensions.dll => 0xa83db366c0e359c2 => 16
	i64 12145679461940342714, ; 204: System.Text.Json => 0xa88e1f1ebcb62fba => 75
	i64 12201331334810686224, ; 205: System.Runtime.Serialization.Primitives.dll => 0xa953d6341e3bd310 => 141
	i64 12269460666702402136, ; 206: System.Collections.Immutable.dll => 0xaa45e178506c9258 => 60
	i64 12279246230491828964, ; 207: SQLitePCLRaw.provider.e_sqlite3.dll => 0xaa68a5636e0512e4 => 57
	i64 12388767885335911387, ; 208: Xamarin.Android.Arch.Lifecycle.LiveData.dll => 0xabedbec0d236dbdb => 82
	i64 12414299427252656003, ; 209: Xamarin.Android.Support.Compat.dll => 0xac48738e28bad783 => 89
	i64 12550732019250633519, ; 210: System.IO.Compression => 0xae2d28465e8e1b2f => 64
	i64 12559163541709922900, ; 211: Xamarin.Android.Support.v7.CardView.dll => 0xae4b1cb32ba82254 => 110
	i64 12708238894395270091, ; 212: System.IO => 0xb05cbbf17d3ba3cb => 137
	i64 12708922737231849740, ; 213: System.Text.Encoding.Extensions => 0xb05f29e50e96e90c => 11
	i64 12717050818822477433, ; 214: System.Runtime.Serialization.Xml.dll => 0xb07c0a5786811679 => 151
	i64 12843321153144804894, ; 215: Microsoft.Extensions.Primitives => 0xb23ca48abd74d61e => 42
	i64 12859742071760748978, ; 216: NControl => 0xb276fb47ca7201b2 => 46
	i64 12952608645614506925, ; 217: Xamarin.Android.Support.Core.Utils => 0xb3c0e8eff48193ad => 92
	i64 12963446364377008305, ; 218: System.Drawing.Common.dll => 0xb3e769c8fd8548b1 => 131
	i64 13105697657173684435, ; 219: ExifLib.dll => 0xb5e0ca950d81b0d3 => 18
	i64 13358059602087096138, ; 220: Xamarin.Android.Support.Fragment.dll => 0xb9615c6f1ee5af4a => 99
	i64 13370592475155966277, ; 221: System.Runtime.Serialization => 0xb98de304062ea945 => 14
	i64 13584560804227020431, ; 222: XLabs.Platform => 0xbc860e13cda5768f => 125
	i64 13647894001087880694, ; 223: System.Data.dll => 0xbd670f48cb071df6 => 128
	i64 13709197460148396571, ; 224: GCS_CPC_EMEC.Android => 0xbe40da749fd5221b => 0
	i64 13818328264475132956, ; 225: Microsoft.Bcl.HashCode => 0xbfc4905809c7c41c => 23
	i64 13861323693514214952, ; 226: NGraphics => 0xc05d5075749ece28 => 51
	i64 13955418299340266673, ; 227: Microsoft.Extensions.DependencyModel.dll => 0xc1ab9b0118299cb1 => 38
	i64 13967638549803255703, ; 228: Xamarin.Forms.Platform.Android => 0xc1d70541e0134797 => 118
	i64 14125464355221830302, ; 229: System.Threading.dll => 0xc407bafdbc707a9e => 4
	i64 14133832980772275001, ; 230: Microsoft.EntityFrameworkCore.dll => 0xc425763635a1c339 => 28
	i64 14254574811015963973, ; 231: System.Text.Encoding.Extensions.dll => 0xc5d26c4442d66545 => 11
	i64 14327695147300244862, ; 232: System.Reflection.dll => 0xc6d632d338eb4d7e => 146
	i64 14369828458497533121, ; 233: Xamarin.Android.Support.Vector.Drawable => 0xc76be2d9300b64c1 => 112
	i64 14400856865250966808, ; 234: Xamarin.Android.Support.Core.UI => 0xc7da1f051a877d18 => 91
	i64 14551742072151931844, ; 235: System.Text.Encodings.Web.dll => 0xc9f22c50f1b8fbc4 => 74
	i64 14661790646341542033, ; 236: Xamarin.Android.Support.SwipeRefreshLayout => 0xcb7924e94e552091 => 106
	i64 14669215534098758659, ; 237: Microsoft.Extensions.DependencyInjection.dll => 0xcb9385ceb3993c03 => 37
	i64 14954917835170835695, ; 238: Microsoft.Extensions.DependencyInjection.Abstractions.dll => 0xcf8a8a895a82ecef => 36
	i64 14987728460634540364, ; 239: System.IO.Compression.dll => 0xcfff1ba06622494c => 64
	i64 15037088180954523232, ; 240: System.Net.Http.Extensions.dll => 0xd0ae7807da04e660 => 67
	i64 15076659072870671916, ; 241: System.ObjectModel.dll => 0xd13b0d8c1620662c => 149
	i64 15133318570858120619, ; 242: System.Net.Http.Primitives.dll => 0xd204590f78d205ab => 69
	i64 15133485256822086103, ; 243: System.Linq.dll => 0xd204f0a9127dd9d7 => 140
	i64 15188640517174936311, ; 244: Xamarin.Android.Arch.Core.Common => 0xd2c8e413d75142f7 => 78
	i64 15227001540531775957, ; 245: Microsoft.Extensions.Configuration.Abstractions.dll => 0xd3512d3999b8e9d5 => 33
	i64 15246441518555807158, ; 246: Xamarin.Android.Arch.Core.Common.dll => 0xd3963dc832493db6 => 78
	i64 15326820765897713587, ; 247: Xamarin.Android.Arch.Core.Runtime.dll => 0xd4b3ce481769e7b3 => 79
	i64 15391712275433856905, ; 248: Microsoft.Extensions.DependencyInjection.Abstractions => 0xd59a58c406411f89 => 36
	i64 15418891414777631748, ; 249: Xamarin.Android.Support.Transition => 0xd5fae80c88241404 => 107
	i64 15457813392950723921, ; 250: Xamarin.Android.Support.Media.Compat => 0xd6852f61c31a8551 => 103
	i64 15526743539506359484, ; 251: System.Text.Encoding.dll => 0xd77a12fc26de2cbc => 7
	i64 15568534730848034786, ; 252: Xamarin.Android.Support.VersionedParcelable.dll => 0xd80e8bda21875fe2 => 113
	i64 15609085926864131306, ; 253: System.dll => 0xd89e9cf3334914ea => 63
	i64 15620595871140898079, ; 254: Microsoft.Extensions.DependencyModel => 0xd8c7812eef49651f => 38
	i64 15661133872274321916, ; 255: System.Xml.ReaderWriter.dll => 0xd9578647d4bfb1fc => 143
	i64 15810740023422282496, ; 256: Xamarin.Forms.Xaml => 0xdb6b08484c22eb00 => 120
	i64 15817206913877585035, ; 257: System.Threading.Tasks.dll => 0xdb8201e29086ac8b => 5
	i64 15963349826457351533, ; 258: System.Threading.Tasks.Extensions => 0xdd893616f748b56d => 2
	i64 16154507427712707110, ; 259: System => 0xe03056ea4e39aa26 => 63
	i64 16242842420508142678, ; 260: Xamarin.Android.Support.CoordinaterLayout.dll => 0xe16a2b1f8908ac56 => 90
	i64 16321164108206115771, ; 261: Microsoft.Extensions.Logging.Abstractions.dll => 0xe2806c487e7b0bbb => 39
	i64 16496768397145114574, ; 262: Mono.Android.Export.dll => 0xe4f04b741db987ce => 44
	i64 16565028646146589191, ; 263: System.ComponentModel.Composition.dll => 0xe5e2cdc9d3bcc207 => 133
	i64 16755018182064898362, ; 264: SQLitePCLRaw.core.dll => 0xe885c843c330813a => 55
	i64 16767985610513713374, ; 265: Xamarin.Android.Arch.Core.Runtime => 0xe8b3da12798808de => 79
	i64 16822611501064131242, ; 266: System.Data.DataSetExtensions => 0xe975ec07bb5412aa => 130
	i64 16833383113903931215, ; 267: mscorlib => 0xe99c30c1484d7f4f => 45
	i64 16890310621557459193, ; 268: System.Text.RegularExpressions.dll => 0xea66700587f088f9 => 138
	i64 16932527889823454152, ; 269: Xamarin.Android.Support.Core.Utils.dll => 0xeafc6c67465253c8 => 92
	i64 17009591894298689098, ; 270: Xamarin.Android.Support.Animated.Vector.Drawable => 0xec0e35b50a097e4a => 85
	i64 17082105670762838071, ; 271: SuaveControls.DynamicStackLayout => 0xed0fd49a49813837 => 58
	i64 17187273293601214786, ; 272: System.ComponentModel.Annotations.dll => 0xee8575ff9aa89142 => 3
	i64 17285063141349522879, ; 273: Rg.Plugins.Popup => 0xefe0e158cc55fdbf => 52
	i64 17333249706306540043, ; 274: System.Diagnostics.Tracing.dll => 0xf08c12c5bb8b920b => 145
	i64 17383232329670771222, ; 275: Xamarin.Android.Support.CustomView.dll => 0xf13da5b41a1cc216 => 95
	i64 17428701562824544279, ; 276: Xamarin.Android.Support.Core.UI.dll => 0xf1df2fbaec73d017 => 91
	i64 17483646997724851973, ; 277: Xamarin.Android.Support.ViewPager => 0xf2a2644fe5b6ef05 => 114
	i64 17524135665394030571, ; 278: Xamarin.Android.Support.Print => 0xf3323c8a739097eb => 104
	i64 17538296339112537137, ; 279: XLabs.Forms => 0xf3648b993a562c31 => 122
	i64 17627500474728259406, ; 280: System.Globalization => 0xf4a176498a351f4e => 10
	i64 17666959971718154066, ; 281: Xamarin.Android.Support.CustomView => 0xf52da67d9f4e4752 => 95
	i64 17685921127322830888, ; 282: System.Diagnostics.Debug.dll => 0xf571038fafa74828 => 6
	i64 17712670374920797664, ; 283: System.Runtime.InteropServices.dll => 0xf5d00bdc38bd3de0 => 144
	i64 17727188866493996799, ; 284: System.Net.Http.Formatting.dll => 0xf603a059f5a25eff => 68
	i64 17760961058993581169, ; 285: Xamarin.Android.Arch.Lifecycle.Common => 0xf67b9bfb46dbac71 => 80
	i64 17777860260071588075, ; 286: System.Runtime.Numerics.dll => 0xf6b7a5b72419c0eb => 9
	i64 17838668724098252521, ; 287: System.Buffers.dll => 0xf78faeb0f5bf3ee9 => 59
	i64 17841643939744178149, ; 288: Xamarin.Android.Arch.Lifecycle.ViewModel => 0xf79a40a25573dfe5 => 84
	i64 17860556028389725318, ; 289: NControl.dll => 0xf7dd71141b219486 => 46
	i64 17882897186074144999, ; 290: FormsViewGroup => 0xf82cd03e3ac830e7 => 19
	i64 17892495832318972303, ; 291: Xamarin.Forms.Xaml.dll => 0xf84eea293687918f => 120
	i64 17928294245072900555, ; 292: System.IO.Compression.FileSystem.dll => 0xf8ce18a0b24011cb => 132
	i64 17936749993673010118, ; 293: Xamarin.Android.Support.Design.dll => 0xf8ec231615deabc6 => 96
	i64 17958105683855786126, ; 294: Xamarin.Android.Arch.Lifecycle.LiveData.Core.dll => 0xf93801f92d25c08e => 81
	i64 18017743553296241350, ; 295: Microsoft.Extensions.Caching.Abstractions => 0xfa0be24cb44e92c6 => 31
	i64 18025913125965088385, ; 296: System.Threading => 0xfa28e87b91334681 => 4
	i64 18090425465832348288, ; 297: Xamarin.Android.Support.v7.RecyclerView => 0xfb0e1a1d2e9e1a80 => 111
	i64 18129453464017766560, ; 298: System.ServiceModel.Internals.dll => 0xfb98c1df1ec108a0 => 136
	i64 18228899564573744928, ; 299: GCS_CPC_EMEC.Android.dll => 0xfcfa0f92b8b0df20 => 0
	i64 18245806341561545090, ; 300: System.Collections.Concurrent.dll => 0xfd3620327d587182 => 13
	i64 18301997741680159453, ; 301: Xamarin.Android.Support.CursorAdapter => 0xfdfdc1fa58d8eadd => 93
	i64 18370042311372477656, ; 302: SQLitePCLRaw.lib.e_sqlite3.android.dll => 0xfeef80274e4094d8 => 56
	i64 18398561365902157495 ; 303: XLabs.Forms.Droid => 0xff54d21520d2e6b7 => 123
], align 16
@assembly_image_cache_indices = local_unnamed_addr constant [304 x i32] [
	i32 26, i32 117, i32 42, i32 43, i32 68, i32 150, i32 50, i32 52, ; 0..7
	i32 84, i32 34, i32 131, i32 15, i32 88, i32 81, i32 56, i32 58, ; 8..15
	i32 129, i32 69, i32 119, i32 147, i32 23, i32 55, i32 51, i32 99, ; 16..23
	i32 14, i32 148, i32 121, i32 113, i32 57, i32 25, i32 49, i32 65, ; 24..31
	i32 109, i32 47, i32 111, i32 22, i32 108, i32 2, i32 49, i32 27, ; 32..39
	i32 134, i32 13, i32 135, i32 74, i32 72, i32 73, i32 76, i32 45, ; 40..47
	i32 37, i32 72, i32 105, i32 116, i32 98, i32 103, i32 151, i32 121, ; 48..55
	i32 71, i32 9, i32 127, i32 35, i32 32, i32 70, i32 73, i32 33, ; 56..63
	i32 143, i32 35, i32 1, i32 40, i32 125, i32 65, i32 141, i32 15, ; 64..71
	i32 150, i32 48, i32 114, i32 107, i32 54, i32 97, i32 87, i32 29, ; 72..79
	i32 136, i32 27, i32 124, i32 3, i32 90, i32 102, i32 12, i32 12, ; 80..87
	i32 25, i32 118, i32 104, i32 115, i32 100, i32 85, i32 6, i32 61, ; 88..95
	i32 86, i32 106, i32 119, i32 19, i32 54, i32 75, i32 102, i32 112, ; 96..103
	i32 123, i32 41, i32 108, i32 134, i32 142, i32 77, i32 71, i32 93, ; 104..111
	i32 140, i32 146, i32 26, i32 77, i32 149, i32 47, i32 116, i32 115, ; 112..119
	i32 66, i32 24, i32 128, i32 87, i32 17, i32 109, i32 31, i32 133, ; 120..127
	i32 8, i32 62, i32 86, i32 130, i32 16, i32 21, i32 8, i32 100, ; 128..135
	i32 139, i32 135, i32 83, i32 60, i32 21, i32 24, i32 18, i32 132, ; 136..143
	i32 62, i32 122, i32 94, i32 101, i32 97, i32 142, i32 67, i32 32, ; 144..151
	i32 28, i32 117, i32 96, i32 137, i32 127, i32 138, i32 70, i32 126, ; 152..159
	i32 124, i32 17, i32 43, i32 129, i32 30, i32 80, i32 126, i32 48, ; 160..167
	i32 147, i32 66, i32 34, i32 101, i32 7, i32 88, i32 22, i32 10, ; 168..175
	i32 98, i32 139, i32 148, i32 29, i32 20, i32 83, i32 94, i32 145, ; 176..183
	i32 39, i32 61, i32 76, i32 41, i32 89, i32 110, i32 30, i32 44, ; 184..191
	i32 144, i32 40, i32 1, i32 53, i32 5, i32 53, i32 50, i32 105, ; 192..199
	i32 82, i32 20, i32 59, i32 16, i32 75, i32 141, i32 60, i32 57, ; 200..207
	i32 82, i32 89, i32 64, i32 110, i32 137, i32 11, i32 151, i32 42, ; 208..215
	i32 46, i32 92, i32 131, i32 18, i32 99, i32 14, i32 125, i32 128, ; 216..223
	i32 0, i32 23, i32 51, i32 38, i32 118, i32 4, i32 28, i32 11, ; 224..231
	i32 146, i32 112, i32 91, i32 74, i32 106, i32 37, i32 36, i32 64, ; 232..239
	i32 67, i32 149, i32 69, i32 140, i32 78, i32 33, i32 78, i32 79, ; 240..247
	i32 36, i32 107, i32 103, i32 7, i32 113, i32 63, i32 38, i32 143, ; 248..255
	i32 120, i32 5, i32 2, i32 63, i32 90, i32 39, i32 44, i32 133, ; 256..263
	i32 55, i32 79, i32 130, i32 45, i32 138, i32 92, i32 85, i32 58, ; 264..271
	i32 3, i32 52, i32 145, i32 95, i32 91, i32 114, i32 104, i32 122, ; 272..279
	i32 10, i32 95, i32 6, i32 144, i32 68, i32 80, i32 9, i32 59, ; 280..287
	i32 84, i32 46, i32 19, i32 120, i32 132, i32 96, i32 81, i32 31, ; 288..295
	i32 4, i32 111, i32 136, i32 0, i32 13, i32 93, i32 56, i32 123 ; 304..303
], align 16

@marshal_methods_number_of_classes = local_unnamed_addr constant i32 0, align 4

; marshal_methods_class_cache
@marshal_methods_class_cache = global [0 x %struct.MarshalMethodsManagedClass] [
], align 8; end of 'marshal_methods_class_cache' array


@get_function_pointer = internal unnamed_addr global void (i32, i32, i32, i8**)* null, align 8

; Function attributes: "frame-pointer"="none" "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" "target-cpu"="x86-64" "target-features"="+cx16,+cx8,+fxsr,+mmx,+popcnt,+sse,+sse2,+sse3,+sse4.1,+sse4.2,+ssse3,+x87" "tune-cpu"="generic" uwtable willreturn writeonly
define void @xamarin_app_init (void (i32, i32, i32, i8**)* %fn) local_unnamed_addr #0
{
	store void (i32, i32, i32, i8**)* %fn, void (i32, i32, i32, i8**)** @get_function_pointer, align 8
	ret void
}

; Names of classes in which marshal methods reside
@mm_class_names = local_unnamed_addr constant [0 x i8*] zeroinitializer, align 8
@__MarshalMethodName_name.0 = internal constant [1 x i8] c"\00", align 1

; mm_method_names
@mm_method_names = local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	; 0
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		i8* getelementptr inbounds ([1 x i8], [1 x i8]* @__MarshalMethodName_name.0, i32 0, i32 0); name
	}
], align 16; end of 'mm_method_names' array


attributes #0 = { "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" uwtable willreturn writeonly "frame-pointer"="none" "target-cpu"="x86-64" "target-features"="+cx16,+cx8,+fxsr,+mmx,+popcnt,+sse,+sse2,+sse3,+sse4.1,+sse4.2,+ssse3,+x87" "tune-cpu"="generic" }
attributes #1 = { "min-legal-vector-width"="0" mustprogress "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" uwtable "frame-pointer"="none" "target-cpu"="x86-64" "target-features"="+cx16,+cx8,+fxsr,+mmx,+popcnt,+sse,+sse2,+sse3,+sse4.1,+sse4.2,+ssse3,+x87" "tune-cpu"="generic" }
attributes #2 = { nounwind }

!llvm.module.flags = !{!0, !1}
!llvm.ident = !{!2}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!2 = !{!"Xamarin.Android remotes/origin/d17-5 @ 45b0e144f73b2c8747d8b5ec8cbd3b55beca67f0"}
!llvm.linker.options = !{}
