AUTOMAKE_OPTIONS=dist-bzip2 dist-zip
BUILD = $(top_builddir)/build

mfgames_utility_ASSEMBLY_NAME = MfGames.Utility
mfgames_utility_DLL = $(mfgames_utility_ASSEMBLY_NAME).dll
mfgames_utility_ASSEMBLY = $(BUILD)/$(mfgames_utility_DLL)
mfgames_utility_FILES =  \
MfGames.Utility/AttributeTreeCollection.cs \
MfGames.Utility/AttributeTree.cs \
MfGames.Utility/Audit/Auditable.cs \
MfGames.Utility/Audit/AuditMessageArgs.cs \
MfGames.Utility/Audit/AuditMessageHandler.cs \
MfGames.Utility/Audit/AuditSeverityArgs.cs \
MfGames.Utility/Audit/AuditSeverityHandler.cs \
MfGames.Utility/Audit/IAuditable.cs \
MfGames.Utility/Audit/NestedAuditable.cs \
MfGames.Utility/ConfigStorage.cs \
MfGames.Utility/Dice/AdditionDice.cs \
MfGames.Utility/Dice/ConstantDice.cs \
MfGames.Utility/Dice/DiceException.cs \
MfGames.Utility/Dice/DiceFactory.cs \
MfGames.Utility/Dice/IDice.cs \
MfGames.Utility/Dice/Parser.cs \
MfGames.Utility/Dice/RandomDice.cs \
MfGames.Utility/Dice/Scanner.cs \
MfGames.Utility/Dice/SubtractionDice.cs \
MfGames.Utility/InvalidPathException.cs \
MfGames.Utility/Log/ConsoleLogger.cs \
MfGames.Utility/Log/DiagnosticSink.cs \
MfGames.Utility/Log/ILogSink.cs \
MfGames.Utility/Log/Logable.cs \
MfGames.Utility/Log/Log.cs \
MfGames.Utility/Log/Logger.cs \
MfGames.Utility/Math/EulerRotation.cs \
MfGames.Utility/Math/Matrix4D.cs \
MfGames.Utility/Math/Quaternion.cs \
MfGames.Utility/Math/Vector2D.cs \
MfGames.Utility/Math/Vector2F.cs \
MfGames.Utility/Math/Vector2I.cs \
MfGames.Utility/Math/Vector3D.cs \
MfGames.Utility/Math/Vector3F.cs \
MfGames.Utility/Math/Vector3I.cs \
MfGames.Utility/MfConvert.cs \
MfGames.Utility/MimeFilenameIdentifier.cs \
MfGames.Utility/NodeRef.cs \
MfGames.Utility/NotAbsolutePathException.cs \
MfGames.Utility/AssemblyInfo.cs \
MfGames.Utility/Random/Entropy.cs \
MfGames.Utility/Random/MersenneRandom.cs \
MfGames.Utility/Random/MersenneTwister.cs \
MfGames.Utility/Random/WeightedSelector.cs \
MfGames.Utility/Settings/ISettings.cs \
MfGames.Utility/Settings/ISettingsStore.cs \
MfGames.Utility/Settings/Settings.cs \
MfGames.Utility/Settings/XmlSettingsStore.cs \
MfGames.Utility/Severity.cs \
MfGames.Utility/Timezone/TimeZones.cs \
MfGames.Utility/Timezone/TimeZoneUS.cs \
MfGames.Utility/Timezone/TimeZoneUtc.cs \
MfGames.Utility/Timing/ITickable.cs \
MfGames.Utility/Timing/SecondsCountdown.cs \
MfGames.Utility/Timing/SecondsGauge.cs \
MfGames.Utility/Timing/TickArgs.cs \
MfGames.Utility/Timing/TickManager.cs \
MfGames.Utility/Tool/ITool.cs \
MfGames.Utility/Tool/OptionalAttribute.cs \
MfGames.Utility/Tool/ParsingArgs.cs \
MfGames.Utility/Tool/PositionalAttribute.cs \
MfGames.Utility/Tool/ToolManager.cs \
MfGames.Utility/Tool/UsageFormatter.cs \
MfGames.Utility/TypeChooser.cs \
MfGames.Utility/UtilityException.cs \
MfGames.Utility/ValueTree.cs \
MfGames.Utility/Version.cs

mfgames_input_ASSEMBLY_NAME = MfGames.Input
mfgames_input_DLL = $(mfgames_input_ASSEMBLY_NAME).dll
mfgames_input_ASSEMBLY = $(BUILD)/$(mfgames_input_DLL)
mfgames_input_FILES =  \
MfGames.Input/InputManager.cs \
MfGames.Input/ChainInputManager.cs \
MfGames.Input/InputTokens.cs \
MfGames.Input/InputEventArgs.cs \
MfGames.Input/TokenMappingDictionary.cs \
MfGames.Input/AssemblyInfo.cs

test_DLL = test.dll
test_ASSEMBLY = $(BUILD)/$(test_DLL)
test_FILES = \
MfGames.Utility/Test/AttributeTreeTest.cs \
MfGames.Utility/Test/AuditTest.cs \
MfGames.Utility/Test/NodeRefTest.cs \
MfGames.Utility/Test/TestDice.cs \
MfGames.Utility/Test/TestTimeZones.cs \
MfGames.Utility/Test/ToolTest.cs \
MfGames.Utility/Test/UtilityTest.cs \
MfGames.Utility/Test/VersionTest.cs 

# Generic
all: $(mfgames_input_ASSEMBLY) $(test_ASSEMBLY)

clean-local:
	$(RM) -rf $(BUILD)
	$(RM) -f *~ */*~ */*/*~
	$(RM) -rf autom4te.cache config.log

$(mfgames_utility_ASSEMBLY): $(mfgames_utility_FILES)
	mkdir -p $(BUILD)
	$(CSC) $(CSC_FLAGS) -target:library -out:$@ \
		-keyfile:etc/MfGames.Utility.snk \
		-doc:$(BUILD)/MfGames.Utility.xml \
		/d:TRACE /d:DEBUG \
		/r:System.Drawing \
		@log4net_LIBS@ \
		$(realpath $(mfgames_utility_FILES))
	chmod a-x $@

$(mfgames_input_ASSEMBLY): $(mfgames_input_FILES) $(mfgames_utility_ASSEMBLY)
	mkdir -p $(BUILD)
	$(CSC) $(CSC_FLAGS) -target:library -out:$@ \
		-keyfile:etc/MfGames.Utility.snk \
		-doc:$(BUILD)/MfGames.Input.xml \
		/d:TRACE /d:DEBUG \
		/r:System.Windows.Forms \
		/r:$(mfgames_utility_ASSEMBLY) \
		$(realpath $(mfgames_input_FILES))
	chmod a-x $@

$(test_ASSEMBLY): $(test_FILES) $(mfgames_utility_ASSEMBLY)
	$(CSC) $(CSC_FLAGS) -target:library -out:$@ \
		@nunit_LIBS@ \
		-r:$(mfgames_utility_ASSEMBLY) \
		$(realpath $(test_FILES))

#	Delete the file if the nunit fails
	@NUNIT_CONSOLE@ /nologo $@ || rm $(test_ASSEMBLY)

#
# Dice
#

MfGames.Utility/Dice/Scanner.cs: MfGames.Utility/Dice/Parser.cs

MfGames.Utility/Dice/Parser.cs: MfGames.Utility/Dice/Dice.atg
MfGames.Utility/Dice/Parser.cs: MfGames.Utility/Dice/Parser.frame
MfGames.Utility/Dice/Parser.cs: MfGames.Utility/Dice/Scanner.frame
	@COCOCS@ -namespace MfGames.Utility.Dice MfGames.Utility/Dice/Dice.atg

#
# Vectors
#

MfGames.Utility/Math/Vector2D.cs: MfGames.Utility/Math/Vector2.cs.in
	cat MfGames.Utility/Math/Vector2.cs.in \
		| sed 's/@SUFFIX@/D/g' \
		| sed 's/@TYPE@/double/g' \
		> MfGames.Utility/Math/Vector2D.cs

MfGames.Utility/Math/Vector2F.cs: MfGames.Utility/Math/Vector2.cs.in
	cat MfGames.Utility/Math/Vector2.cs.in \
		| sed 's/@SUFFIX@/F/g' \
		| sed 's/@TYPE@/float/g' \
		> MfGames.Utility/Math/Vector2F.cs

MfGames.Utility/Math/Vector2I.cs: MfGames.Utility/Math/Vector2.cs.in
	cat MfGames.Utility/Math/Vector2.cs.in \
		| sed 's/@SUFFIX@/I/g' \
		| sed 's/@TYPE@/int/g' \
		> MfGames.Utility/Math/Vector2I.cs

MfGames.Utility/Math/Vector3D.cs: MfGames.Utility/Math/Vector3.cs.in
	cat MfGames.Utility/Math/Vector3.cs.in \
		| sed 's/@SUFFIX@/D/g' \
		| sed 's/@TYPE@/double/g' \
		> MfGames.Utility/Math/Vector3D.cs

MfGames.Utility/Math/Vector3F.cs: MfGames.Utility/Math/Vector3.cs.in
	cat MfGames.Utility/Math/Vector3.cs.in \
		| sed 's/@SUFFIX@/F/g' \
		| sed 's/@TYPE@/float/g' \
		> MfGames.Utility/Math/Vector3F.cs

MfGames.Utility/Math/Vector3I.cs: MfGames.Utility/Math/Vector3.cs.in
	cat MfGames.Utility/Math/Vector3.cs.in \
		| sed 's/@SUFFIX@/I/g' \
		| sed 's/@TYPE@/int/g' \
		> MfGames.Utility/Math/Vector3I.cs

# pkgconfig
pkgconfig_in_files = etc/mfgames-utility-3.0.pc.in etc/mfgames-input-3.0.pc.in
pkgconfigdir= $(prefix)/lib/pkgconfig
pkgconfig_DATA = $(pkgconfig_in_files:.pc.in=.pc)

# Installation
install-data-local:
	@GACUTIL@ /i $(mfgames_utility_ASSEMBLY) /f @GACUTIL_FLAGS@
	@GACUTIL@ /i $(mfgames_input_ASSEMBLY) /f @GACUTIL_FLAGS@

	mkdir -p @libdir@/cli/mfgames-utility-3.0
	install $(mfgames_utility_ASSEMBLY) @libdir@/cli/mfgames-utility-3.0/
	install $(mfgames_input_ASSEMBLY) @libdir@/cli/mfgames-utility-3.0/

# Uninstallation
uninstall-local:
	@GACUTIL@ /u $(mfgames_utility_ASSEMBLY_NAME) @GACUTIL_FLAGS@
	@GACUTIL@ /u $(mfgames_input_ASSEMBLY_NAME) @GACUTIL_FLAGS@

	-rm @libdir@/cli/mfgames-utility-3.0/$(mfgames_input_DLL)
	-rm @libdir@/cli/mfgames-utility-3.0/$(mfgames_utility_DLL)
	-rmdir @libdir@/cli/mfgames-utility-3.0

# Definition of the various files that need to be cleaned up properly.
CLEANFILES = $(BUILD)/MfGames.Utility.xml TestResult.xml

EXTRA_DIST = $(pkgconfig_in_files) etc/MfGames.Utility.snk
EXTRA_DIST += MfGames.Utility/Dice/Dice.atg MfGames.Utility/Dice/Parser.frame MfGames.Utility/Dice/Scanner.frame
EXTRA_DIST += $(mfgames_utility_FILES) $(mfgames_input_FILES)
EXTRA_DIST += $(test_FILES)

DISTCLEANFILES = etc/mfgames-utility-3.0.pc etc/mfgames-input-3.0.pc
DISTCLEANFILES += MfGames.Utility/AssemblyInfo.cs MfGames.Input/AssemblyInfo.cs
