directory = "C:/Users/Juan Jaspe/Documents/Octave/Crypto/";
addpath([directory "ML Trainer"]);
addpath([directory "ML Trainer/Functions"]);
addpath([directory "ML Trainer/Feature Transforms"]);
addpath([directory "Data"]);
%% Initialization
clear ; close all; clc

%% Setup the parameters you will use for this part of the exercise
input_layer_size  = 6;  
num_labels = 10;        
                        

% Load Training Data
fprintf('Loading and Visualizing Data ...\n')

featuresData = load('features.txt'); % training data stored in arrays X
X = featuresData(:, [1, 2, 3, 4, 5 ,6, 7, 8, 9]);
[X, m, s] = normalizeFeatures(X);

resultsData = load('results.txt');
Y = resultsData(:,[1]);


% =============================== SINGLE============================
[m, n] = size(X);
X=[ones(m, 1) X];

theta_t = zeros(n+1, 1);
lambda = 0.1;
%train model
options = optimset('GradObj', 'on', 'MaxIter', 50);
all_theta = zeros(num_labels, n+1);
for c = -5:5
  [theta]=fmincg(@(t)(lrCostFunction(t, X, (Y > c).*(Y <= (c+1)), lambda)), theta_t,  options);
  all_theta(c+6,:) = theta;
endfor 



%% ================ Part 3: Predict for One-Vs-All ================
disp(all_theta);
pred = predictOneVsAll(all_theta, X);

fprintf('\nTraining Set Accuracy: %f\n', mean(double(Y > pred').*double(Y <= (pred'.+1))) * 100);

 %% ================ QUAD ==================================================

QuadX = QuadratizeFeatures(X(:,2:end));
[m, n] = size(QuadX);
QuadX=[ones(m, 1) QuadX];

theta_t = zeros(n+1, 1);

fprintf('\n theta %f',n);
lambda = 0.1;
%train model
options = optimset('GradObj', 'on', 'MaxIter', 50);
qall_theta = zeros(num_labels, n+1);


for c = -5:5
  [theta]=fmincg(@(t)(lrCostFunction(t, QuadX, (Y > c).*(Y <= (c+1)), lambda)), theta_t,  options);
   qall_theta(c+6,:) = theta;
endfor 



%% ================ Part 3: Predict for One-Vs-All ================

pred = predictOneVsAll(qall_theta, QuadX);

fprintf('\nTraining Set Accuracy: %f\n', mean(double(Y > pred').*double(Y <= pred'.+1)) * 100);

