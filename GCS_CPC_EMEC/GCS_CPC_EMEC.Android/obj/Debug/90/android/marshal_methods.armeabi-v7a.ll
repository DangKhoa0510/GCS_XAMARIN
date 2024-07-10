; ModuleID = 'obj\Debug\90\android\marshal_methods.armeabi-v7a.ll'
source_filename = "obj\Debug\90\android\marshal_methods.armeabi-v7a.ll"
target datalayout = "e-m:e-p:32:32-Fi8-i64:64-v128:64:128-a:0:32-n32-S64"
target triple = "armv7-unknown-linux-android"


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
@assembly_image_cache = local_unnamed_addr global [0 x %struct.MonoImage*] zeroinitializer, align 4
; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = local_unnamed_addr constant [304 x i32] [
	i32 26230656, ; 0: Microsoft.Extensions.DependencyModel => 0x1903f80 => 38
	i32 39109920, ; 1: Newtonsoft.Json.dll => 0x254c520 => 49
	i32 57263871, ; 2: Xamarin.Forms.Core.dll => 0x369c6ff => 116
	i32 57967248, ; 3: Xamarin.Android.Support.VersionedParcelable.dll => 0x3748290 => 113
	i32 60076830, ; 4: XLabs.Platform.Droid => 0x394b31e => 126
	i32 89217283, ; 5: XLabs.Core.dll => 0x5515903 => 121
	i32 117431740, ; 6: System.Runtime.InteropServices => 0x6ffddbc => 144
	i32 159306688, ; 7: System.ComponentModel.Annotations => 0x97ed3c0 => 3
	i32 160529393, ; 8: Xamarin.Android.Arch.Core.Common => 0x9917bf1 => 78
	i32 166922606, ; 9: Xamarin.Android.Support.Compat.dll => 0x9f3096e => 89
	i32 185010651, ; 10: System.Net.Http.Primitives => 0xb0709db => 69
	i32 201930040, ; 11: Xamarin.Android.Arch.Core.Runtime => 0xc093538 => 79
	i32 205061960, ; 12: System.ComponentModel => 0xc38ff48 => 15
	i32 219130465, ; 13: Xamarin.Android.Support.v4 => 0xd0faa61 => 108
	i32 220171995, ; 14: System.Diagnostics.Debug => 0xd1f8edb => 6
	i32 230752869, ; 15: Microsoft.CSharp.dll => 0xdc10265 => 24
	i32 231814094, ; 16: System.Globalization => 0xdd133ce => 10
	i32 232815796, ; 17: System.Web.Services => 0xde07cb4 => 134
	i32 293914992, ; 18: Xamarin.Android.Support.Transition => 0x1184c970 => 107
	i32 297432599, ; 19: GCS_CPC_EMEC.Android.dll => 0x11ba7617 => 0
	i32 321597661, ; 20: System.Numerics => 0x132b30dd => 70
	i32 347068432, ; 21: SQLitePCLRaw.lib.e_sqlite3.android.dll => 0x14afd810 => 56
	i32 381494705, ; 22: Xamarin.Forms.Material => 0x16bd25b1 => 117
	i32 385762202, ; 23: System.Memory.dll => 0x16fe439a => 65
	i32 388313361, ; 24: Xamarin.Android.Support.Animated.Vector.Drawable => 0x17253111 => 85
	i32 389971796, ; 25: Xamarin.Android.Support.Core.UI => 0x173e7f54 => 91
	i32 398767139, ; 26: SuaveControls.DynamicStackLayout => 0x17c4b423 => 58
	i32 416859308, ; 27: XLabs.Forms.dll => 0x18d8c4ac => 122
	i32 442521989, ; 28: Xamarin.Essentials => 0x1a605985 => 115
	i32 442565967, ; 29: System.Collections => 0x1a61054f => 8
	i32 459347974, ; 30: System.Runtime.Serialization.Primitives.dll => 0x1b611806 => 141
	i32 465846621, ; 31: mscorlib => 0x1bc4415d => 45
	i32 469710990, ; 32: System.dll => 0x1bff388e => 63
	i32 498788369, ; 33: System.ObjectModel => 0x1dbae811 => 149
	i32 513247710, ; 34: Microsoft.Extensions.Primitives.dll => 0x1e9789de => 42
	i32 514659665, ; 35: Xamarin.Android.Support.Compat => 0x1ead1551 => 89
	i32 524864063, ; 36: Xamarin.Android.Support.Print => 0x1f48ca3f => 104
	i32 526420162, ; 37: System.Transactions.dll => 0x1f6088c2 => 129
	i32 539058512, ; 38: Microsoft.Extensions.Logging => 0x20216150 => 40
	i32 539750087, ; 39: Xamarin.Android.Support.Design => 0x202beec7 => 96
	i32 545304856, ; 40: System.Runtime.Extensions => 0x2080b118 => 139
	i32 546455878, ; 41: System.Runtime.Serialization.Xml => 0x20924146 => 151
	i32 548916678, ; 42: Microsoft.Bcl.AsyncInterfaces => 0x20b7cdc6 => 22
	i32 571524804, ; 43: Xamarin.Android.Support.v7.RecyclerView => 0x2210c6c4 => 111
	i32 605376203, ; 44: System.IO.Compression.FileSystem => 0x24154ecb => 132
	i32 662205335, ; 45: System.Text.Encodings.Web.dll => 0x27787397 => 74
	i32 672442732, ; 46: System.Collections.Concurrent => 0x2814a96c => 13
	i32 690569205, ; 47: System.Xml.Linq.dll => 0x29293ff5 => 77
	i32 692692150, ; 48: Xamarin.Android.Support.Annotations => 0x2949a4b6 => 86
	i32 748832960, ; 49: SQLitePCLRaw.batteries_v2 => 0x2ca248c0 => 54
	i32 775507847, ; 50: System.IO.Compression => 0x2e394f87 => 64
	i32 789151979, ; 51: Microsoft.Extensions.Options => 0x2f0980eb => 41
	i32 801787702, ; 52: Xamarin.Android.Support.Interpolator => 0x2fca4f36 => 100
	i32 809851609, ; 53: System.Drawing.Common.dll => 0x30455ad9 => 131
	i32 877678880, ; 54: System.Globalization.dll => 0x34505120 => 10
	i32 882883187, ; 55: Xamarin.Android.Support.v4.dll => 0x349fba73 => 108
	i32 902159924, ; 56: Rg.Plugins.Popup => 0x35c5de34 => 52
	i32 916714535, ; 57: Xamarin.Android.Support.Print.dll => 0x36a3f427 => 104
	i32 918404897, ; 58: NControl.dll => 0x36bdbf21 => 46
	i32 955402788, ; 59: Newtonsoft.Json => 0x38f24a24 => 49
	i32 958213972, ; 60: Xamarin.Android.Support.Media.Compat => 0x391d2f54 => 103
	i32 974778368, ; 61: FormsViewGroup.dll => 0x3a19f000 => 19
	i32 975236339, ; 62: System.Diagnostics.Tracing => 0x3a20ecf3 => 145
	i32 975874589, ; 63: System.Xml.XDocument => 0x3a2aaa1d => 142
	i32 987342438, ; 64: Xamarin.Android.Arch.Lifecycle.LiveData.dll => 0x3ad9a666 => 82
	i32 992768348, ; 65: System.Collections.dll => 0x3b2c715c => 8
	i32 1028951442, ; 66: Microsoft.Extensions.DependencyInjection.Abstractions => 0x3d548d92 => 36
	i32 1042160112, ; 67: Xamarin.Forms.Platform.dll => 0x3e1e19f0 => 119
	i32 1044663988, ; 68: System.Linq.Expressions.dll => 0x3e444eb4 => 150
	i32 1098167829, ; 69: Xamarin.Android.Arch.Lifecycle.ViewModel => 0x4174b615 => 84
	i32 1098259244, ; 70: System => 0x41761b2c => 63
	i32 1099692271, ; 71: Microsoft.DotNet.PlatformAbstractions => 0x418bf8ef => 26
	i32 1157931901, ; 72: Microsoft.EntityFrameworkCore.Abstractions => 0x4504a37d => 27
	i32 1158878630, ; 73: GCS_CPC_EMEC.dll => 0x451315a6 => 20
	i32 1186231468, ; 74: Newtonsoft.Json.Bson.dll => 0x46b474ac => 48
	i32 1202000627, ; 75: Microsoft.EntityFrameworkCore.Abstractions.dll => 0x47a512f3 => 27
	i32 1204575371, ; 76: Microsoft.Extensions.Caching.Memory.dll => 0x47cc5c8b => 32
	i32 1219235520, ; 77: XLabs.Ioc.dll => 0x48ac0ec0 => 124
	i32 1292207520, ; 78: SQLitePCLRaw.core.dll => 0x4d0585a0 => 55
	i32 1292763917, ; 79: Xamarin.Android.Support.CursorAdapter.dll => 0x4d0e030d => 93
	i32 1297413738, ; 80: Xamarin.Android.Arch.Lifecycle.LiveData.Core => 0x4d54f66a => 81
	i32 1319735448, ; 81: GCS_CPC_EMEC => 0x4ea99098 => 20
	i32 1324164729, ; 82: System.Linq => 0x4eed2679 => 140
	i32 1359785034, ; 83: Xamarin.Android.Support.Design.dll => 0x510cac4a => 96
	i32 1364015309, ; 84: System.IO => 0x514d38cd => 137
	i32 1365406463, ; 85: System.ServiceModel.Internals.dll => 0x516272ff => 136
	i32 1379779777, ; 86: System.Resources.ResourceManager => 0x523dc4c1 => 12
	i32 1411638395, ; 87: System.Runtime.CompilerServices.Unsafe => 0x5423e47b => 72
	i32 1430110960, ; 88: XLabs.Forms => 0x553dc2f0 => 122
	i32 1445445088, ; 89: Xamarin.Android.Support.Fragment => 0x5627bde0 => 99
	i32 1457743152, ; 90: System.Runtime.Extensions.dll => 0x56e36530 => 139
	i32 1460219004, ; 91: Xamarin.Forms.Xaml => 0x57092c7c => 120
	i32 1461234159, ; 92: System.Collections.Immutable.dll => 0x5718a9ef => 60
	i32 1462112819, ; 93: System.IO.Compression.dll => 0x57261233 => 64
	i32 1470490898, ; 94: Microsoft.Extensions.Primitives => 0x57a5e912 => 42
	i32 1479771757, ; 95: System.Collections.Immutable => 0x5833866d => 60
	i32 1490351284, ; 96: Microsoft.Data.Sqlite.dll => 0x58d4f4b4 => 25
	i32 1543031311, ; 97: System.Text.RegularExpressions.dll => 0x5bf8ca0f => 138
	i32 1550322496, ; 98: System.Reflection.Extensions.dll => 0x5c680b40 => 16
	i32 1574652163, ; 99: Xamarin.Android.Support.Core.Utils.dll => 0x5ddb4903 => 92
	i32 1587447679, ; 100: Xamarin.Android.Arch.Core.Common.dll => 0x5e9e877f => 78
	i32 1592978981, ; 101: System.Runtime.Serialization.dll => 0x5ef2ee25 => 14
	i32 1636319766, ; 102: ExifLib.dll => 0x61884216 => 18
	i32 1639515021, ; 103: System.Net.Http.dll => 0x61b9038d => 66
	i32 1639986890, ; 104: System.Text.RegularExpressions => 0x61c036ca => 138
	i32 1657153582, ; 105: System.Runtime => 0x62c6282e => 73
	i32 1662014763, ; 106: Xamarin.Android.Support.Vector.Drawable => 0x6310552b => 112
	i32 1677501392, ; 107: System.Net.Primitives.dll => 0x63fca3d0 => 148
	i32 1688112883, ; 108: Microsoft.Data.Sqlite => 0x649e8ef3 => 25
	i32 1689493916, ; 109: Microsoft.EntityFrameworkCore.dll => 0x64b3a19c => 28
	i32 1701541528, ; 110: System.Diagnostics.Debug.dll => 0x656b7698 => 6
	i32 1711441057, ; 111: SQLitePCLRaw.lib.e_sqlite3.android => 0x660284a1 => 56
	i32 1712866787, ; 112: NControl => 0x661845e3 => 46
	i32 1726116996, ; 113: System.Reflection.dll => 0x66e27484 => 146
	i32 1746316138, ; 114: Mono.Android.Export => 0x6816ab6a => 44
	i32 1765942094, ; 115: System.Reflection.Extensions => 0x6942234e => 16
	i32 1770582343, ; 116: Microsoft.Extensions.Logging.dll => 0x6988f147 => 40
	i32 1776026572, ; 117: System.Core.dll => 0x69dc03cc => 61
	i32 1796073524, ; 118: NGraphics.Android => 0x6b0de834 => 50
	i32 1796167890, ; 119: Microsoft.Bcl.AsyncInterfaces.dll => 0x6b0f58d2 => 22
	i32 1824175904, ; 120: System.Text.Encoding.Extensions => 0x6cbab720 => 11
	i32 1828688058, ; 121: Microsoft.Extensions.Logging.Abstractions.dll => 0x6cff90ba => 39
	i32 1858542181, ; 122: System.Linq.Expressions => 0x6ec71a65 => 150
	i32 1866717392, ; 123: Xamarin.Android.Support.Interpolator.dll => 0x6f43d8d0 => 100
	i32 1867746548, ; 124: Xamarin.Essentials.dll => 0x6f538cf4 => 115
	i32 1877418711, ; 125: Xamarin.Android.Support.v7.RecyclerView.dll => 0x6fe722d7 => 111
	i32 1878053835, ; 126: Xamarin.Forms.Xaml.dll => 0x6ff0d3cb => 120
	i32 1886040351, ; 127: Microsoft.EntityFrameworkCore.Sqlite.dll => 0x706ab11f => 30
	i32 1894524299, ; 128: Microsoft.DotNet.PlatformAbstractions.dll => 0x70ec258b => 26
	i32 1900610850, ; 129: System.Resources.ResourceManager.dll => 0x71490522 => 12
	i32 1916660109, ; 130: Xamarin.Android.Arch.Lifecycle.ViewModel.dll => 0x723de98d => 84
	i32 1968388702, ; 131: Microsoft.Extensions.Configuration.dll => 0x75533a5e => 35
	i32 1988461382, ; 132: XLabs.Forms.Droid.dll => 0x76858346 => 123
	i32 2011961780, ; 133: System.Buffers.dll => 0x77ec19b4 => 59
	i32 2014489277, ; 134: Microsoft.EntityFrameworkCore.Sqlite => 0x7812aabd => 30
	i32 2037417872, ; 135: Xamarin.Android.Support.ViewPager => 0x79708790 => 114
	i32 2044222327, ; 136: Xamarin.Android.Support.Loader => 0x79d85b77 => 101
	i32 2048278909, ; 137: Microsoft.Extensions.Configuration.Binder.dll => 0x7a16417d => 34
	i32 2069514766, ; 138: Newtonsoft.Json.Bson => 0x7b5a4a0e => 48
	i32 2079903147, ; 139: System.Runtime.dll => 0x7bf8cdab => 73
	i32 2090596640, ; 140: System.Numerics.Vectors => 0x7c9bf920 => 71
	i32 2103459038, ; 141: SQLitePCLRaw.provider.e_sqlite3.dll => 0x7d603cde => 57
	i32 2126786730, ; 142: Xamarin.Forms.Platform.Android => 0x7ec430aa => 118
	i32 2139458754, ; 143: Xamarin.Android.Support.DrawerLayout => 0x7f858cc2 => 98
	i32 2166116741, ; 144: Xamarin.Android.Support.Core.Utils => 0x811c5185 => 92
	i32 2181898931, ; 145: Microsoft.Extensions.Options.dll => 0x820d22b3 => 41
	i32 2192057212, ; 146: Microsoft.Extensions.Logging.Abstractions => 0x82a8237c => 39
	i32 2193016926, ; 147: System.ObjectModel.dll => 0x82b6c85e => 149
	i32 2196165013, ; 148: Xamarin.Android.Support.VersionedParcelable => 0x82e6d195 => 113
	i32 2197979891, ; 149: Microsoft.Extensions.DependencyModel.dll => 0x830282f3 => 38
	i32 2201231467, ; 150: System.Net.Http => 0x8334206b => 66
	i32 2252897993, ; 151: Microsoft.EntityFrameworkCore => 0x86487ec9 => 28
	i32 2266799131, ; 152: Microsoft.Extensions.Configuration.Abstractions => 0x871c9c1b => 33
	i32 2330457430, ; 153: Xamarin.Android.Support.Core.UI.dll => 0x8ae7f556 => 91
	i32 2330986192, ; 154: Xamarin.Android.Support.SlidingPaneLayout.dll => 0x8af006d0 => 105
	i32 2353062107, ; 155: System.Net.Primitives => 0x8c40e0db => 148
	i32 2361609489, ; 156: NGraphics => 0x8cc34d11 => 51
	i32 2368005991, ; 157: System.Xml.ReaderWriter.dll => 0x8d24e767 => 143
	i32 2371007202, ; 158: Microsoft.Extensions.Configuration => 0x8d52b2e2 => 35
	i32 2373288475, ; 159: Xamarin.Android.Support.Fragment.dll => 0x8d75821b => 99
	i32 2392818924, ; 160: System.Net.Http.Formatting.dll => 0x8e9f84ec => 68
	i32 2397331369, ; 161: NControl.Droid.dll => 0x8ee45fa9 => 47
	i32 2410286270, ; 162: XLabs.Serialization => 0x8faa0cbe => 127
	i32 2435904999, ; 163: System.ComponentModel.DataAnnotations.dll => 0x9130f5e7 => 135
	i32 2440966767, ; 164: Xamarin.Android.Support.Loader.dll => 0x917e326f => 101
	i32 2454642406, ; 165: System.Text.Encoding.dll => 0x924edee6 => 7
	i32 2465273461, ; 166: SQLitePCLRaw.batteries_v2.dll => 0x92f11675 => 54
	i32 2471841756, ; 167: netstandard.dll => 0x93554fdc => 1
	i32 2475788418, ; 168: Java.Interop.dll => 0x93918882 => 21
	i32 2487632829, ; 169: Xamarin.Android.Support.DocumentFile => 0x944643bd => 97
	i32 2501346920, ; 170: System.Data.DataSetExtensions => 0x95178668 => 130
	i32 2562349572, ; 171: Microsoft.CSharp => 0x98ba5a04 => 24
	i32 2563143864, ; 172: AndHUD.dll => 0x98c678b8 => 17
	i32 2570120770, ; 173: System.Text.Encodings.Web => 0x9930ee42 => 74
	i32 2585220780, ; 174: System.Text.Encoding.Extensions.dll => 0x9a1756ac => 11
	i32 2614234196, ; 175: XLabs.Core => 0x9bd20c54 => 121
	i32 2629600743, ; 176: System.Net.Http.Extensions.dll => 0x9cbc85e7 => 67
	i32 2634653062, ; 177: Microsoft.EntityFrameworkCore.Relational.dll => 0x9d099d86 => 29
	i32 2642760363, ; 178: NControl.Droid => 0x9d8552ab => 47
	i32 2664396074, ; 179: System.Xml.XDocument.dll => 0x9ecf752a => 142
	i32 2686887180, ; 180: System.Runtime.Serialization.Xml.dll => 0xa026a50c => 151
	i32 2693849962, ; 181: System.IO.dll => 0xa090e36a => 137
	i32 2698266930, ; 182: Xamarin.Android.Arch.Lifecycle.LiveData => 0xa0d44932 => 82
	i32 2715334215, ; 183: System.Threading.Tasks.dll => 0xa1d8b647 => 5
	i32 2724373263, ; 184: System.Runtime.Numerics.dll => 0xa262a30f => 9
	i32 2744603572, ; 185: XLabs.Platform.dll => 0xa39753b4 => 125
	i32 2751899777, ; 186: Xamarin.Android.Support.Collections => 0xa406a881 => 88
	i32 2766581644, ; 187: Xamarin.Forms.Core => 0xa4e6af8c => 116
	i32 2782647110, ; 188: Xamarin.Android.Support.CustomTabs.dll => 0xa5dbd346 => 94
	i32 2788639665, ; 189: Xamarin.Android.Support.LocalBroadcastManager => 0xa63743b1 => 102
	i32 2788775637, ; 190: Xamarin.Android.Support.SwipeRefreshLayout.dll => 0xa63956d5 => 106
	i32 2819470561, ; 191: System.Xml.dll => 0xa80db4e1 => 76
	i32 2847789619, ; 192: Microsoft.EntityFrameworkCore.Relational => 0xa9bdd233 => 29
	i32 2861098320, ; 193: Mono.Android.Export.dll => 0xaa88e550 => 44
	i32 2861816565, ; 194: Rg.Plugins.Popup.dll => 0xaa93daf5 => 52
	i32 2873651463, ; 195: NGraphics.dll => 0xab487107 => 51
	i32 2876493027, ; 196: Xamarin.Android.Support.SwipeRefreshLayout => 0xab73cce3 => 106
	i32 2893257763, ; 197: Xamarin.Android.Arch.Core.Runtime.dll => 0xac739c23 => 79
	i32 2901442782, ; 198: System.Reflection => 0xacf080de => 146
	i32 2903344695, ; 199: System.ComponentModel.Composition => 0xad0d8637 => 133
	i32 2905242038, ; 200: mscorlib.dll => 0xad2a79b6 => 45
	i32 2919462931, ; 201: System.Numerics.Vectors.dll => 0xae037813 => 71
	i32 2921692953, ; 202: Xamarin.Android.Support.CustomView.dll => 0xae257f19 => 95
	i32 2922925221, ; 203: Xamarin.Android.Support.Vector.Drawable.dll => 0xae384ca5 => 112
	i32 2959614098, ; 204: System.ComponentModel.dll => 0xb0682092 => 15
	i32 3044182254, ; 205: FormsViewGroup => 0xb57288ee => 19
	i32 3056250942, ; 206: Xamarin.Android.Support.AsyncLayoutInflater.dll => 0xb62ab03e => 87
	i32 3068715062, ; 207: Xamarin.Android.Arch.Lifecycle.Common => 0xb6e8e036 => 80
	i32 3069363400, ; 208: Microsoft.Extensions.Caching.Abstractions.dll => 0xb6f2c4c8 => 31
	i32 3075834255, ; 209: System.Threading.Tasks => 0xb755818f => 5
	i32 3092211740, ; 210: Xamarin.Android.Support.Media.Compat.dll => 0xb84f681c => 103
	i32 3111772706, ; 211: System.Runtime.Serialization => 0xb979e222 => 14
	i32 3124832203, ; 212: System.Threading.Tasks.Extensions => 0xba4127cb => 2
	i32 3147165239, ; 213: System.Diagnostics.Tracing.dll => 0xbb95ee37 => 145
	i32 3191408315, ; 214: Xamarin.Android.Support.CustomTabs => 0xbe3906bb => 94
	i32 3195844289, ; 215: Microsoft.Extensions.Caching.Abstractions => 0xbe7cb6c1 => 31
	i32 3204380047, ; 216: System.Data.dll => 0xbefef58f => 128
	i32 3204912593, ; 217: Xamarin.Android.Support.AsyncLayoutInflater => 0xbf0715d1 => 87
	i32 3220365878, ; 218: System.Threading => 0xbff2e236 => 4
	i32 3233339011, ; 219: Xamarin.Android.Arch.Lifecycle.LiveData.Core.dll => 0xc0b8d683 => 81
	i32 3247949154, ; 220: Mono.Security => 0xc197c562 => 147
	i32 3265893370, ; 221: System.Threading.Tasks.Extensions.dll => 0xc2a993fa => 2
	i32 3268915939, ; 222: ExifLib => 0xc2d7b2e3 => 18
	i32 3280506390, ; 223: System.ComponentModel.Annotations.dll => 0xc3888e16 => 3
	i32 3286872994, ; 224: SQLite-net.dll => 0xc3e9b3a2 => 53
	i32 3296380511, ; 225: Xamarin.Android.Support.Collections.dll => 0xc47ac65f => 88
	i32 3299363146, ; 226: System.Text.Encoding => 0xc4a8494a => 7
	i32 3317144872, ; 227: System.Data => 0xc5b79d28 => 128
	i32 3321585405, ; 228: Xamarin.Android.Support.DocumentFile.dll => 0xc5fb5efd => 97
	i32 3352662376, ; 229: Xamarin.Android.Support.CoordinaterLayout => 0xc7d59168 => 90
	i32 3357663996, ; 230: Xamarin.Android.Support.CursorAdapter => 0xc821e2fc => 93
	i32 3358260929, ; 231: System.Text.Json => 0xc82afec1 => 75
	i32 3360279109, ; 232: SQLitePCLRaw.core => 0xc849ca45 => 55
	i32 3366347497, ; 233: Java.Interop => 0xc8a662e9 => 21
	i32 3395150330, ; 234: System.Runtime.CompilerServices.Unsafe.dll => 0xca5de1fa => 72
	i32 3404865022, ; 235: System.ServiceModel.Internals => 0xcaf21dfe => 136
	i32 3421170118, ; 236: Microsoft.Extensions.Configuration.Binder => 0xcbeae9c6 => 34
	i32 3428513518, ; 237: Microsoft.Extensions.DependencyInjection.dll => 0xcc5af6ee => 37
	i32 3429136800, ; 238: System.Xml => 0xcc6479a0 => 76
	i32 3430777524, ; 239: netstandard => 0xcc7d82b4 => 1
	i32 3439690031, ; 240: Xamarin.Android.Support.Annotations.dll => 0xcd05812f => 86
	i32 3442543374, ; 241: AndHUD => 0xcd310b0e => 17
	i32 3452182196, ; 242: GCS_CPC_EMEC.Android => 0xcdc41eb4 => 0
	i32 3476120550, ; 243: Mono.Android => 0xcf3163e6 => 43
	i32 3485117614, ; 244: System.Text.Json.dll => 0xcfbaacae => 75
	i32 3486566296, ; 245: System.Transactions => 0xcfd0c798 => 129
	i32 3498942916, ; 246: Xamarin.Android.Support.v7.CardView.dll => 0xd08da1c4 => 110
	i32 3509114376, ; 247: System.Xml.Linq => 0xd128d608 => 77
	i32 3522916314, ; 248: System.Net.Http.Extensions => 0xd1fb6fda => 67
	i32 3536029504, ; 249: Xamarin.Forms.Platform.Android.dll => 0xd2c38740 => 118
	i32 3547625832, ; 250: Xamarin.Android.Support.SlidingPaneLayout => 0xd3747968 => 105
	i32 3567349600, ; 251: System.ComponentModel.Composition.dll => 0xd4a16f60 => 133
	i32 3584434553, ; 252: SuaveControls.DynamicStackLayout.dll => 0xd5a62179 => 58
	i32 3608519521, ; 253: System.Linq.dll => 0xd715a361 => 140
	i32 3632359727, ; 254: Xamarin.Forms.Platform => 0xd881692f => 119
	i32 3645089577, ; 255: System.ComponentModel.DataAnnotations => 0xd943a729 => 135
	i32 3657292374, ; 256: Microsoft.Extensions.Configuration.Abstractions.dll => 0xd9fdda56 => 33
	i32 3664423555, ; 257: Xamarin.Android.Support.ViewPager.dll => 0xda6aaa83 => 114
	i32 3672681054, ; 258: Mono.Android.dll => 0xdae8aa5e => 43
	i32 3676310014, ; 259: System.Web.Services.dll => 0xdb2009fe => 134
	i32 3678221644, ; 260: Xamarin.Android.Support.v7.AppCompat => 0xdb3d354c => 109
	i32 3681174138, ; 261: Xamarin.Android.Arch.Lifecycle.Common.dll => 0xdb6a427a => 80
	i32 3685813676, ; 262: Xamarin.Forms.Material.dll => 0xdbb10dac => 117
	i32 3689375977, ; 263: System.Drawing.Common => 0xdbe768e9 => 131
	i32 3714038699, ; 264: Xamarin.Android.Support.LocalBroadcastManager.dll => 0xdd5fbbab => 102
	i32 3718463572, ; 265: Xamarin.Android.Support.Animated.Vector.Drawable.dll => 0xdda34054 => 85
	i32 3748608112, ; 266: System.Diagnostics.DiagnosticSource => 0xdf6f3870 => 62
	i32 3754567612, ; 267: SQLitePCLRaw.provider.e_sqlite3 => 0xdfca27bc => 57
	i32 3770675850, ; 268: XLabs.Serialization.dll => 0xe0bff28a => 127
	i32 3776062606, ; 269: Xamarin.Android.Support.DrawerLayout.dll => 0xe112248e => 98
	i32 3798658073, ; 270: System.Net.Http.Primitives.dll => 0xe26aec19 => 69
	i32 3829621856, ; 271: System.Numerics.dll => 0xe4436460 => 70
	i32 3832731800, ; 272: Xamarin.Android.Support.CoordinaterLayout.dll => 0xe472d898 => 90
	i32 3841636137, ; 273: Microsoft.Extensions.DependencyInjection.Abstractions.dll => 0xe4fab729 => 36
	i32 3849253459, ; 274: System.Runtime.InteropServices.dll => 0xe56ef253 => 144
	i32 3862817207, ; 275: Xamarin.Android.Arch.Lifecycle.Runtime.dll => 0xe63de9b7 => 83
	i32 3874897629, ; 276: Xamarin.Android.Arch.Lifecycle.Runtime => 0xe6f63edd => 83
	i32 3876362041, ; 277: SQLite-net => 0xe70c9739 => 53
	i32 3883175360, ; 278: Xamarin.Android.Support.v7.AppCompat.dll => 0xe7748dc0 => 109
	i32 3894448521, ; 279: Microsoft.Bcl.HashCode => 0xe8209189 => 23
	i32 3896106733, ; 280: System.Collections.Concurrent.dll => 0xe839deed => 13
	i32 3920810846, ; 281: System.IO.Compression.FileSystem.dll => 0xe9b2d35e => 132
	i32 3928044579, ; 282: System.Xml.ReaderWriter => 0xea213423 => 143
	i32 3945713374, ; 283: System.Data.DataSetExtensions.dll => 0xeb2ecede => 130
	i32 4025784931, ; 284: System.Memory => 0xeff49a63 => 65
	i32 4063435586, ; 285: Xamarin.Android.Support.CustomView => 0xf2331b42 => 95
	i32 4067712627, ; 286: NGraphics.Android.dll => 0xf2745e73 => 50
	i32 4073602200, ; 287: System.Threading.dll => 0xf2ce3c98 => 4
	i32 4074728431, ; 288: XLabs.Platform => 0xf2df6bef => 125
	i32 4101842092, ; 289: Microsoft.Extensions.Caching.Memory => 0xf47d24ac => 32
	i32 4105002889, ; 290: Mono.Security.dll => 0xf4ad5f89 => 147
	i32 4124256450, ; 291: XLabs.Ioc => 0xf5d328c2 => 124
	i32 4126470640, ; 292: Microsoft.Extensions.DependencyInjection => 0xf5f4f1f0 => 37
	i32 4131741489, ; 293: System.Net.Http.Formatting => 0xf6455f31 => 68
	i32 4151237749, ; 294: System.Core => 0xf76edc75 => 61
	i32 4163576315, ; 295: XLabs.Platform.Droid.dll => 0xf82b21fb => 126
	i32 4181436372, ; 296: System.Runtime.Serialization.Primitives => 0xf93ba7d4 => 141
	i32 4213026141, ; 297: System.Diagnostics.DiagnosticSource.dll => 0xfb1dad5d => 62
	i32 4213894455, ; 298: XLabs.Forms.Droid => 0xfb2aed37 => 123
	i32 4216993138, ; 299: Xamarin.Android.Support.Transition.dll => 0xfb5a3572 => 107
	i32 4219003402, ; 300: Xamarin.Android.Support.v7.CardView => 0xfb78e20a => 110
	i32 4260525087, ; 301: System.Buffers => 0xfdf2741f => 59
	i32 4263658931, ; 302: Microsoft.Bcl.HashCode.dll => 0xfe2245b3 => 23
	i32 4274976490 ; 303: System.Runtime.Numerics => 0xfecef6ea => 9
], align 4
@assembly_image_cache_indices = local_unnamed_addr constant [304 x i32] [
	i32 38, i32 49, i32 116, i32 113, i32 126, i32 121, i32 144, i32 3, ; 0..7
	i32 78, i32 89, i32 69, i32 79, i32 15, i32 108, i32 6, i32 24, ; 8..15
	i32 10, i32 134, i32 107, i32 0, i32 70, i32 56, i32 117, i32 65, ; 16..23
	i32 85, i32 91, i32 58, i32 122, i32 115, i32 8, i32 141, i32 45, ; 24..31
	i32 63, i32 149, i32 42, i32 89, i32 104, i32 129, i32 40, i32 96, ; 32..39
	i32 139, i32 151, i32 22, i32 111, i32 132, i32 74, i32 13, i32 77, ; 40..47
	i32 86, i32 54, i32 64, i32 41, i32 100, i32 131, i32 10, i32 108, ; 48..55
	i32 52, i32 104, i32 46, i32 49, i32 103, i32 19, i32 145, i32 142, ; 56..63
	i32 82, i32 8, i32 36, i32 119, i32 150, i32 84, i32 63, i32 26, ; 64..71
	i32 27, i32 20, i32 48, i32 27, i32 32, i32 124, i32 55, i32 93, ; 72..79
	i32 81, i32 20, i32 140, i32 96, i32 137, i32 136, i32 12, i32 72, ; 80..87
	i32 122, i32 99, i32 139, i32 120, i32 60, i32 64, i32 42, i32 60, ; 88..95
	i32 25, i32 138, i32 16, i32 92, i32 78, i32 14, i32 18, i32 66, ; 96..103
	i32 138, i32 73, i32 112, i32 148, i32 25, i32 28, i32 6, i32 56, ; 104..111
	i32 46, i32 146, i32 44, i32 16, i32 40, i32 61, i32 50, i32 22, ; 112..119
	i32 11, i32 39, i32 150, i32 100, i32 115, i32 111, i32 120, i32 30, ; 120..127
	i32 26, i32 12, i32 84, i32 35, i32 123, i32 59, i32 30, i32 114, ; 128..135
	i32 101, i32 34, i32 48, i32 73, i32 71, i32 57, i32 118, i32 98, ; 136..143
	i32 92, i32 41, i32 39, i32 149, i32 113, i32 38, i32 66, i32 28, ; 144..151
	i32 33, i32 91, i32 105, i32 148, i32 51, i32 143, i32 35, i32 99, ; 152..159
	i32 68, i32 47, i32 127, i32 135, i32 101, i32 7, i32 54, i32 1, ; 160..167
	i32 21, i32 97, i32 130, i32 24, i32 17, i32 74, i32 11, i32 121, ; 168..175
	i32 67, i32 29, i32 47, i32 142, i32 151, i32 137, i32 82, i32 5, ; 176..183
	i32 9, i32 125, i32 88, i32 116, i32 94, i32 102, i32 106, i32 76, ; 184..191
	i32 29, i32 44, i32 52, i32 51, i32 106, i32 79, i32 146, i32 133, ; 192..199
	i32 45, i32 71, i32 95, i32 112, i32 15, i32 19, i32 87, i32 80, ; 200..207
	i32 31, i32 5, i32 103, i32 14, i32 2, i32 145, i32 94, i32 31, ; 208..215
	i32 128, i32 87, i32 4, i32 81, i32 147, i32 2, i32 18, i32 3, ; 216..223
	i32 53, i32 88, i32 7, i32 128, i32 97, i32 90, i32 93, i32 75, ; 224..231
	i32 55, i32 21, i32 72, i32 136, i32 34, i32 37, i32 76, i32 1, ; 232..239
	i32 86, i32 17, i32 0, i32 43, i32 75, i32 129, i32 110, i32 77, ; 240..247
	i32 67, i32 118, i32 105, i32 133, i32 58, i32 140, i32 119, i32 135, ; 248..255
	i32 33, i32 114, i32 43, i32 134, i32 109, i32 80, i32 117, i32 131, ; 256..263
	i32 102, i32 85, i32 62, i32 57, i32 127, i32 98, i32 69, i32 70, ; 264..271
	i32 90, i32 36, i32 144, i32 83, i32 83, i32 53, i32 109, i32 23, ; 272..279
	i32 13, i32 132, i32 143, i32 130, i32 65, i32 95, i32 50, i32 4, ; 280..287
	i32 125, i32 32, i32 147, i32 124, i32 37, i32 68, i32 61, i32 126, ; 288..295
	i32 141, i32 62, i32 123, i32 107, i32 110, i32 59, i32 23, i32 9 ; 304..303
], align 4

@marshal_methods_number_of_classes = local_unnamed_addr constant i32 0, align 4

; marshal_methods_class_cache
@marshal_methods_class_cache = global [0 x %struct.MarshalMethodsManagedClass] [
], align 4; end of 'marshal_methods_class_cache' array


@get_function_pointer = internal unnamed_addr global void (i32, i32, i32, i8**)* null, align 4

; Function attributes: "frame-pointer"="all" "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" "target-cpu"="generic" "target-features"="+armv7-a,+d32,+dsp,+fp64,+neon,+thumb-mode,+vfp2,+vfp2sp,+vfp3,+vfp3d16,+vfp3d16sp,+vfp3sp,-aes,-fp-armv8,-fp-armv8d16,-fp-armv8d16sp,-fp-armv8sp,-fp16,-fp16fml,-fullfp16,-sha2,-vfp4,-vfp4d16,-vfp4d16sp,-vfp4sp" uwtable willreturn writeonly
define void @xamarin_app_init (void (i32, i32, i32, i8**)* %fn) local_unnamed_addr #0
{
	store void (i32, i32, i32, i8**)* %fn, void (i32, i32, i32, i8**)** @get_function_pointer, align 4
	ret void
}

; Names of classes in which marshal methods reside
@mm_class_names = local_unnamed_addr constant [0 x i8*] zeroinitializer, align 4
@__MarshalMethodName_name.0 = internal constant [1 x i8] c"\00", align 1

; mm_method_names
@mm_method_names = local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	; 0
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		i8* getelementptr inbounds ([1 x i8], [1 x i8]* @__MarshalMethodName_name.0, i32 0, i32 0); name
	}
], align 8; end of 'mm_method_names' array


attributes #0 = { "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" uwtable willreturn writeonly "frame-pointer"="all" "target-cpu"="generic" "target-features"="+armv7-a,+d32,+dsp,+fp64,+neon,+thumb-mode,+vfp2,+vfp2sp,+vfp3,+vfp3d16,+vfp3d16sp,+vfp3sp,-aes,-fp-armv8,-fp-armv8d16,-fp-armv8d16sp,-fp-armv8sp,-fp16,-fp16fml,-fullfp16,-sha2,-vfp4,-vfp4d16,-vfp4d16sp,-vfp4sp" }
attributes #1 = { "min-legal-vector-width"="0" mustprogress "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" uwtable "frame-pointer"="all" "target-cpu"="generic" "target-features"="+armv7-a,+d32,+dsp,+fp64,+neon,+thumb-mode,+vfp2,+vfp2sp,+vfp3,+vfp3d16,+vfp3d16sp,+vfp3sp,-aes,-fp-armv8,-fp-armv8d16,-fp-armv8d16sp,-fp-armv8sp,-fp16,-fp16fml,-fullfp16,-sha2,-vfp4,-vfp4d16,-vfp4d16sp,-vfp4sp" }
attributes #2 = { nounwind }

!llvm.module.flags = !{!0, !1, !2}
!llvm.ident = !{!3}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!2 = !{i32 1, !"min_enum_size", i32 4}
!3 = !{!"Xamarin.Android remotes/origin/d17-5 @ 45b0e144f73b2c8747d8b5ec8cbd3b55beca67f0"}
!llvm.linker.options = !{}
