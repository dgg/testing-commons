properties {
	$configuration = 'Debug'
	$base_dir  = resolve-path .	
    $release_path = "$base_dir\release"
}

task default -depends Compile

task Compile { 
    msbuild .\Testing.Commons.sln
}

task Test {

}