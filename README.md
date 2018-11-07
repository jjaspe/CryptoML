# CryptoML

*You need Octave, preferable Octave UI

1. Start your mongo db
2. Modify "directory" value in appSettings in FeaturesGatherer to point to your ML Trainer folder
3. Run FeaturesGatherer
4. Open ML Predictor\ML Trainer\runner.m in Octave
5. Modify directory on top to point to your local "ML Predictor" directory 
6. In Octaves console, enter addpath(\<directory\>), where \<directory\> is the same as you entered in runner.m
7. In console, run "runner".
